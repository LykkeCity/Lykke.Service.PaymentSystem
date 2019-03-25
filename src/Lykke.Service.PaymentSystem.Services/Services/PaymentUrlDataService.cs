using System;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Contracts.Payments;
using Lykke.Payments.Client;
using Lykke.Payments.Contracts;
using Lykke.Service.ClientAccount.Client.AutorestClient.Models;
using Lykke.Service.PaymentSystem.Core.Constants;
using Lykke.Service.PaymentSystem.Core.Domain.PaymentUrlData;
using Lykke.Service.PaymentSystem.Core.Repositories;
using Lykke.Service.PaymentSystem.Core.Services;
using Lykke.Service.PaymentSystem.Core.Settings.ServiceSettings.PaymentSystem;
using Lykke.Service.PaymentSystem.Services.Domain;
using Newtonsoft.Json;

namespace Lykke.Service.PaymentSystem.Services.Services
{
    public class PaymentUrlDataService : IPaymentUrlDataService, IService
    {
        private readonly IOwnerTypeService _ownerTypeService;
        private readonly ILegalEntityService _legalEntityService;
        private readonly FxpaygateSettings _fxpaygateSettings;
        private readonly CreditVouchersSettings _creditVouchersSettings;
        private readonly EasyPaymentGatewaySettings _easyPaymentGatewaySettings;
        private readonly IIdentityRepository _identityRepository;

        public PaymentUrlDataService(
            PaymentSettings paymentSettings, 
            IOwnerTypeService ownerTypeService, 
            ILegalEntityService legalEntityService, 
            IIdentityRepository identityRepository)
        {
            _ownerTypeService = ownerTypeService;
            _legalEntityService = legalEntityService;
            _identityRepository = identityRepository;
            _fxpaygateSettings = paymentSettings.Fxpaygate;
            _creditVouchersSettings = paymentSettings.CreditVouchers;
            _easyPaymentGatewaySettings = paymentSettings.EasyPaymentGateway;
        }

        public async Task<PaymentUrlData> GetUrlDataAsync(
            string paymentSystem, 
            string transactionId, 
            string clientId, 
            double fullAmount, 
            string assetId,
            string walletId, 
            string countryIso3Code, 
            string info)
        {
            var paymentSystemSelection = await SelectPaymentSystemAsync(walletId, assetId, countryIso3Code, paymentSystem);

            GetUrlDataResult urlData;
            using (var paymentGatewayService = new PaymentGatewayServiceClient(paymentSystemSelection.ServiceUrl))
            {
                urlData = await paymentGatewayService.GetUrlData(transactionId, clientId, fullAmount, assetId, info);
            }

            var result = new PaymentUrlData
            {
                PaymentUrl = urlData.PaymentUrl,
                OkUrl = urlData.OkUrl,
                FailUrl = urlData.FailUrl,
                CancelUrl = JsonConvert.DeserializeObject<OtherPaymentInfo>(info).CancelUrl,
                ReloadRegexp = urlData.ReloadRegexp,
                UrlsRegexp = urlData.UrlsRegexp,
                ErrorMessage = urlData.ErrorMessage,
                PaymentSystem = paymentSystemSelection.PaymentSystem,
            };

            return result;
        }

        public async Task<string> GenerateNewTransactionIdAsync()
        {
            return (await _identityRepository.GenerateNewIdAsync()).ToString();
        }

        public async Task<string> GetSourceClientIdAsync(string walletId, string clientPaymentSystem)
        {
            var ownerType = await _ownerTypeService.GetOwnerTypeAsync(walletId);

            var legalEntity = ownerType == OwnerType.Mt
                ? await _legalEntityService.GetLegalEntityAsync(walletId)
                : string.Empty;

            var serviceUrl = clientPaymentSystem == CashInPaymentSystem.CreditVoucher.ToString()
                ? SelectCreditVouchersService()
                : SelectEasyPaymentGateWayService(ownerType, legalEntity);

            using (var paymentService = new PaymentGatewayServiceClient(serviceUrl))
            {
                return await paymentService.GetSourceClientId();
            }
        }

        private bool IsPaymentSystemSupported(CashInPaymentSystem paymentSystem, string assetId)
        {
            switch (paymentSystem)
            {
                case CashInPaymentSystem.CreditVoucher:
                    return _creditVouchersSettings.SupportedCurrencies?.Contains(assetId) ?? assetId == LykkeConstants.UsdAssetId;
                case CashInPaymentSystem.Fxpaygate:
                    return _fxpaygateSettings.SupportedCurrencies?.Contains(assetId) ?? assetId == LykkeConstants.UsdAssetId;
                case CashInPaymentSystem.EasyPaymentGateway:
                    return _easyPaymentGatewaySettings.SupportedCurrencies?.Contains(assetId) ?? assetId == LykkeConstants.UsdAssetId;
                default:
                    return false;
            }
        }

        private bool IsEasyPaymentGatewayAndSpot(string clientPaymentSystem, string assetId, string isoCountryCode)
        {
            var byClient = CardPaymentSystem.Unknown;
            var hasClientFixedSystem = false;

            if (!string.IsNullOrWhiteSpace(clientPaymentSystem))
                hasClientFixedSystem = Enum.TryParse(clientPaymentSystem, out byClient);

            return hasClientFixedSystem && byClient == CardPaymentSystem.EasyPaymentGateway
                   || (!hasClientFixedSystem || byClient != CardPaymentSystem.CreditVoucher)
                   && _easyPaymentGatewaySettings.SupportedCurrencies.Contains(assetId)
                   && _easyPaymentGatewaySettings.Countries.Contains(isoCountryCode);
        }

        private string SelectEasyPaymentGateWayService(OwnerType owner, string legalEntity = null)
        {
            var key = owner + (string.IsNullOrEmpty(legalEntity) ? "" : $"_{legalEntity}");

            if (!_easyPaymentGatewaySettings.ServiceUrls.TryGetValue(key, out var result))
            {
                throw new NotSupportedException($"Owner {owner} is not supported by FxPaygate");
            }

            return result;
        }

        private async Task<PaymentSystemSelectionResult> SelectPaymentSystemAsync(string walletId, string assetId, string countryCode, string clientPaymentSystem)
        {
            var paymentSystem = CashInPaymentSystem.CreditVoucher;
            var serviceUrl = SelectCreditVouchersService();

            var ownerType = await _ownerTypeService.GetOwnerTypeAsync(walletId);

            if (ownerType == OwnerType.Mt)
            {
                var legalEntity = await _legalEntityService.GetLegalEntityAsync(walletId);

                paymentSystem = CashInPaymentSystem.EasyPaymentGateway;
                serviceUrl = SelectEasyPaymentGateWayService(OwnerType.Mt, legalEntity);
            }
            else if (IsEasyPaymentGatewayAndSpot(clientPaymentSystem, assetId, countryCode))
            {
                paymentSystem = CashInPaymentSystem.EasyPaymentGateway;
                serviceUrl = SelectEasyPaymentGateWayService(OwnerType.Spot);
            }

            if (!IsPaymentSystemSupported(paymentSystem, assetId))
            {
                throw new ArgumentException($"Asset {assetId} is not supported by {paymentSystem} payment system.");
            }

            return new PaymentSystemSelectionResult
            {
                PaymentSystem = paymentSystem,
                ServiceUrl = serviceUrl
            };
        }

        private string SelectCreditVouchersService()
        {
            return _creditVouchersSettings.ServiceUrls[0];
        }
    }
}
