using System;
using System.Net;
using System.Threading.Tasks;
using Lykke.Contracts.Payments;
using Lykke.Payments.Client;
using Lykke.Payments.Contracts;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using Lykke.Service.ClientAccount.Client.AutorestClient.Models;
using Lykke.Service.PaymentSystem.Core.Services;
using Lykke.Service.PaymentSystem.Models;

namespace Lykke.Service.PaymentSystem.Controllers
{
    [Route("api/[controller]")]
    public class PaymentUrlDataController : Controller
    {
        private readonly IPaymentUrlDataService _paymentUrlDataService;
        private readonly IOwnerTypeService _ownerTypeService;
        private readonly ILegalEntityService _legalEntityService;

        public PaymentUrlDataController(
            IPaymentUrlDataService paymentUrlDataService,
            ILegalEntityService legalEntityService,
            IOwnerTypeService ownerTypeService)
        {
            _paymentUrlDataService = paymentUrlDataService;
            _legalEntityService = legalEntityService;
            _ownerTypeService = ownerTypeService;
        }

        [HttpPost]
        [SwaggerOperation("PostPaymentUrlData")]
        [ProducesResponseType(typeof(PaymentUrlDataResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post(PaymentUrlDataRequest model)
        {
            var walletId = model.WalletId;
            var assetId = model.AssetId;

            var paymentSystem = CashInPaymentSystem.CreditVoucher;
            var serviceUrl = _paymentUrlDataService.SelectCreditVouchersService();

            var ownerType = await _ownerTypeService.GetOwnerTypeAsync(walletId);

            if (ownerType == OwnerType.Mt)
            {
                var legalEntity = await _legalEntityService.GetLegalEntityAsync(walletId);

                paymentSystem = CashInPaymentSystem.Fxpaygate;
                serviceUrl = _paymentUrlDataService.SelectFxpaygateService(OwnerType.Mt, legalEntity);
            }
            else if (_paymentUrlDataService.IsFxpaygateAndSpot(model.ClientPaymentSystem, assetId, model.IsoCountryCode))
            {
                paymentSystem = CashInPaymentSystem.Fxpaygate;
                serviceUrl = _paymentUrlDataService.SelectFxpaygateService(OwnerType.Spot);
            }

            if (!_paymentUrlDataService.IsPaymentSystemSupported(paymentSystem, assetId))
                throw new Exception($"Asset {assetId} is not supported by {paymentSystem} payment system.");

            GetUrlDataResult urlData;
            using (var paymentGatewayService = new PaymentGatewayServiceClient(serviceUrl))
            {
                urlData = await paymentGatewayService.GetUrlData(model.OrderId, model.ClientId, model.Amount, assetId, model.OtherInfoJson);
            }

            var result = new PaymentUrlDataResponse
            {
                PaymentUrl = urlData.PaymentUrl,
                OkUrl = urlData.OkUrl,
                FailUrl = urlData.FailUrl,
                ReloadRegexp = urlData.ReloadRegexp,
                UrlsRegexp = urlData.UrlsRegexp,
                ErrorMessage = urlData.ErrorMessage,
                PaymentSystem = paymentSystem,
            };
            return Ok(result);
        }
    }
}
