using System;
using System.Net;
using System.Threading.Tasks;
using Common;
using Common.Log;
using Lykke.Contracts.Payments;
using Lykke.Service.Assets.Client;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using Lykke.Service.FeeCalculator.Client;
using Lykke.Service.PaymentSystem.Core.Components;
using Lykke.Service.PaymentSystem.Core.Constants;
using Lykke.Service.PaymentSystem.Core.Enums;
using Lykke.Service.PaymentSystem.Core.Services;
using Lykke.Service.PaymentSystem.Models;
using Lykke.Service.PersonalData.Contract;
using Lykke.Common.Api.Contract.Responses;

namespace Lykke.Service.PaymentSystem.Controllers
{
    [Route("api/[controller]")]
    public class PaymentUrlDataController : Controller
    {
        private readonly IPaymentUrlDataService _paymentUrlDataService;
        private readonly IPaymentTransactionEventLogService _paymentTransactionEventLogService;
        private readonly IPaymentTransactionsService _paymentTransactionsService;
        private readonly ICountryComponent _countryComponent;

        private readonly IAssetsService _assetsService;
        private readonly IFeeCalculatorClient _feeCalculatorClient;
        private readonly IPersonalDataService _personalDataService;
        private readonly ILog _log;

        public PaymentUrlDataController(
            IPaymentUrlDataService paymentUrlDataService,
            IPaymentTransactionEventLogService paymentTransactionEventLogService,
            IPaymentTransactionsService paymentTransactionsService,
            ILog log,
            IAssetsService assetsService,
            IFeeCalculatorClient feeCalculatorClient,
            IPersonalDataService personalDataService, 
            ICountryComponent countryComponent)
        {
            _paymentUrlDataService = paymentUrlDataService;
            _paymentTransactionEventLogService = paymentTransactionEventLogService;
            _paymentTransactionsService = paymentTransactionsService;
            _log = log;
            _assetsService = assetsService;
            _feeCalculatorClient = feeCalculatorClient;
            _personalDataService = personalDataService;
            _countryComponent = countryComponent;
        }

        [HttpPost]
        [SwaggerOperation("PostPaymentUrlData")]
        [ProducesResponseType(typeof(PaymentUrlDataResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(PaymentUrlDataRequest model)
        {
            await _log.WriteWarningAsync("Test log", "", "Incoming request for payment url data");

            if (string.IsNullOrWhiteSpace(model.AssetId))
                model.AssetId = LykkeConstants.UsdAssetId;

            var phoneNumberE164 = model.Phone.PreparePhoneNum().ToE164Number();
            var countryAsIso3 = _countryComponent.GetCountryIso3Code(model.Country);
            var pd = await _personalDataService.GetAsync(model.ClientId);

            CashInPaymentSystem paymentSystem;

            switch (model.DepositOption)
            {
                case DepositOption.BankCard:
                    paymentSystem = CashInPaymentSystem.EasyPaymentGateway;
                    break;
                case DepositOption.Other:
                    paymentSystem = CashInPaymentSystem.CreditVoucher;
                    break;
                default:
                    paymentSystem = CashInPaymentSystem.Unknown;
                    break;
            }

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
                    model.FailUrl,
                    model.CancelUrl)
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
                await _log.WriteWarningAsync(nameof(PaymentUrlDataController), nameof(Post), model.ToJson(),
                    urlData.ErrorMessage, DateTime.UtcNow);

                return BadRequest(ErrorResponse.Create(urlData.ErrorMessage));
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
                FailUrl = urlData.FailUrl,
                CancelUrl = urlData.CancelUrl,
            };

            return Ok(result);
        }

        [HttpGet]
        [Route("SourceClientId")]
        [SwaggerOperation("GetSourceClientId")]
        [ProducesResponseType(typeof(SourceClientInfoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetSourceClientId(string walletId, string clientPaymentSystem)
        {
            if (string.IsNullOrEmpty(walletId))
                return BadRequest(ErrorResponse.Create($"{nameof(walletId)} id can't be empty"));

            if (string.IsNullOrEmpty(clientPaymentSystem))
                return BadRequest(ErrorResponse.Create($"{nameof(clientPaymentSystem)} id can't be empty"));

            var clientId = await _paymentUrlDataService.GetSourceClientIdAsync(walletId, clientPaymentSystem);

            return Ok(new SourceClientInfoResponse { SourceClientId = clientId });
        }
    }
}
