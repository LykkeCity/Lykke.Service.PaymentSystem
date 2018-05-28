using System;
using System.Net;
using System.Threading.Tasks;
using Common;
using Common.Log;
using Lykke.Contracts.Payments;
using Lykke.Service.Assets.Client;
using Lykke.Service.FeeCalculator.Client;
using Lykke.Service.PaymentSystem.Core.Components;
using Lykke.Service.PaymentSystem.Core.Constants;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using Lykke.Service.PaymentSystem.Core.Services;
using Lykke.Service.PaymentSystem.Models;
using Lykke.Service.PersonalData.Contract;

namespace Lykke.Service.PaymentSystem.Controllers
{
    [Route("api/[controller]")]
    public class DepositController : Controller
    {
        private readonly IPaymentTransactionsService _paymentTransactionsService;
        private readonly IPersonalDataService _personalDataService;
        private readonly ICountryComponent _countryComponent;
        private readonly IPaymentUrlDataService _paymentUrlDataService;
        private readonly IPaymentTransactionEventLogService _paymentTransactionEventLogService;
        private readonly IAssetsService _assetsService;
        private readonly IFeeCalculatorClient _feeCalculatorClient;
        private readonly ILog _log;

        public DepositController(
            IPaymentTransactionsService paymentTransactionsService, 
            IPersonalDataService personalDataService, 
            ICountryComponent countryComponent, IPaymentUrlDataService paymentUrlDataService, IPaymentTransactionEventLogService paymentTransactionEventLogService, IAssetsService assetsService, IFeeCalculatorClient feeCalculatorClient, ILog log)
        {
            _paymentTransactionsService = paymentTransactionsService;
            _personalDataService = personalDataService;
            _countryComponent = countryComponent;
            _paymentUrlDataService = paymentUrlDataService;
            _paymentTransactionEventLogService = paymentTransactionEventLogService;
            _assetsService = assetsService;
            _feeCalculatorClient = feeCalculatorClient;
            _log = log;
        }

        [HttpGet]
        [Route("FxPaygate/Last")]
        [SwaggerOperation("Last")]
        [ProducesResponseType(typeof(PaymentTransactionResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Last(string clientId)
        {
            var lastPaymentTransaction = await _paymentTransactionsService.GetLastByDateAsync(clientId);

            var personalData = await _personalDataService.GetAsync(clientId);

            if (personalData == null)
            {
                return BadRequest("A user with such an clientId does not exist.");
            }

            var isCreditVoucherOrFxpaygate = lastPaymentTransaction != null
                    && (lastPaymentTransaction.PaymentSystem == CashInPaymentSystem.CreditVoucher
                        || lastPaymentTransaction.PaymentSystem == CashInPaymentSystem.Fxpaygate);

            var result = isCreditVoucherOrFxpaygate
                ? PaymentTransactionResponse.Create(lastPaymentTransaction, personalData)
                : PaymentTransactionResponse.Create(personalData);

            result.Country = _countryComponent.GetCountryIso3Code(result.Country);

            return Ok(result);
        }

        [HttpPost]
        [Route("FxPaygate/PaymentUrl")]
        [SwaggerOperation("PaymentUrl")]
        [ProducesResponseType(typeof(PaymentUrlDataResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PaymentUrl(PaymentUrlDataRequest model)
        {
            if (string.IsNullOrWhiteSpace(model.AssetId))
                model.AssetId = LykkeConstants.UsdAssetId;

            var phoneNumberE164 = model.Phone.PreparePhoneNum().ToE164Number();
            var countryAsIso3 = _countryComponent.GetCountryIso3Code(model.Country);
            var pd = await _personalDataService.GetAsync(model.ClientId);

            CashInPaymentSystem paymentSystem = CashInPaymentSystem.Fxpaygate; ;

            var transactionId = await _paymentUrlDataService.GenerateNewTransactionIdAsync();

            const string formatOfDateOfBirth = "yyyy-MM-dd";

            var info = OtherPaymentInfo.Create(
                    model.FirstName,
                    model.LastName,
                    model.City,
                    model.Zip,
                    model.Address,
                    countryAsIso3,
                    model.Email,
                    phoneNumberE164,
                    pd.DateOfBirth?.ToString(formatOfDateOfBirth),
                    model.OkUrl,
                    model.FailUrl)
                .ToJson();

            var bankCardsFee = await _feeCalculatorClient.GetBankCardFees();

            var asset = await _assetsService.AssetGetAsync(model.AssetId);
            var feeAmount = Math.Round(model.Amount * bankCardsFee.Percentage, 15);
            var feeAmountTruncated = feeAmount.TruncateDecimalPlaces(asset.Accuracy, true);

            var urlData = await _paymentUrlDataService.GetUrlDataAsync(
                paymentSystem.ToString(),
                transactionId,
                model.ClientId,
                model.Amount + feeAmountTruncated,
                model.AssetId,
                model.WalletId,
                countryAsIso3,
                info);

            await _paymentTransactionEventLogService.InsertPaymentTransactionEventLogAsync(new PaymentTransactionEventLog
            {
                PaymentTransactionId = transactionId,
                Message = "Payment Url has created",
                DateTime = DateTime.UtcNow,
                TechData = urlData.PaymentUrl,
                Who = model.ClientId
            });

            if (!string.IsNullOrEmpty(urlData.ErrorMessage))
            {
                await _log.WriteWarningAsync(nameof(DepositController), nameof(PaymentUrl), model.ToJson(),
                    urlData.ErrorMessage, DateTime.UtcNow);

                return BadRequest(new
                {
                    message = urlData.ErrorMessage
                });
            }

            await _paymentTransactionsService.InsertPaymentTransactionAsync(
                new PaymentTransaction
                {
                    Amount = model.Amount,
                    Status = PaymentStatus.Created,
                    PaymentSystem = paymentSystem,
                    FeeAmount = feeAmountTruncated,
                    Id = transactionId,
                    ClientId = model.ClientId,
                    AssetId = model.AssetId,
                    DepositedAssetId = model.AssetId,
                    WalletId = model.WalletId,
                    Created = DateTime.UtcNow,
                    Info = info
                });

            await _paymentTransactionEventLogService.InsertPaymentTransactionEventLogAsync(new PaymentTransactionEventLog
            {
                PaymentTransactionId = transactionId,
                Message = "Registered",
                DateTime = DateTime.UtcNow,
                TechData = string.Empty,
                Who = model.ClientId
            });

            // mode=iframe is for Mobile version 
            if (!string.IsNullOrWhiteSpace(urlData.PaymentUrl))
                urlData.PaymentUrl = urlData.PaymentUrl
                                     + (urlData.PaymentUrl.Contains("?") ? "&" : "?")
                                     + "mode=iframe";

            var result = new PaymentUrlDataResponse
            {
                Url = urlData.PaymentUrl,
                OkUrl = urlData.OkUrl,
                FailUrl = urlData.FailUrl
            };

            return Ok(result);
        }
    }
}
