using System;
using System.Linq;
using Lykke.Contracts.Payments;
using Lykke.Service.ClientAccount.Client.AutorestClient.Models;
using Lykke.Service.PaymentSystem.Core.Constants;
using Lykke.Service.PaymentSystem.Core.Services;
using Lykke.Service.PaymentSystem.Core.Settings.ServiceSettings.PaymentSystem;

namespace Lykke.Service.PaymentSystem.Services.Services
{
    public class PaymentUrlDataService : IPaymentUrlDataService, IService
    {
        private readonly FxpaygateSettings _fxpaygateSettings;
        private readonly CreditVouchersSettings _creditVouchersSettings;

        public PaymentUrlDataService(PaymentSettings paymentSettings)
        {
            _fxpaygateSettings = paymentSettings.Fxpaygate;
            _creditVouchersSettings = paymentSettings.CreditVouchers;
        }

        public bool IsFxpaygateAndSpot(string clientPaymentSystem, string assetId, string isoCountryCode)
        {
            var byClient = CardPaymentSystem.Unknown;
            var hasClientFixedSystem = false;

            if (!string.IsNullOrWhiteSpace(clientPaymentSystem))
                hasClientFixedSystem = Enum.TryParse(clientPaymentSystem, out byClient);

            return hasClientFixedSystem && byClient == CardPaymentSystem.Fxpaygate
                   || (!hasClientFixedSystem || byClient != CardPaymentSystem.CreditVoucher)
                       && _fxpaygateSettings.Currencies.Contains(assetId)
                       && _fxpaygateSettings.Countries.Contains(isoCountryCode);
        }

        public bool IsPaymentSystemSupported(CashInPaymentSystem paymentSystem, string assetId)
        {
            switch (paymentSystem)
            {
                case CashInPaymentSystem.CreditVoucher:
                    return _creditVouchersSettings.SupportedCurrencies?.Contains(assetId) ?? assetId == LykkeConstants.UsdAssetId;
                case CashInPaymentSystem.Fxpaygate:
                    return _fxpaygateSettings.SupportedCurrencies?.Contains(assetId) ?? assetId == LykkeConstants.UsdAssetId;
                default:
                    return false;
            }
        }

        public string SelectFxpaygateService(OwnerType owner, string legalEntity = null)
        {
            var key = owner + (string.IsNullOrEmpty(legalEntity) ? "" : $"_{legalEntity}");

            if (!_fxpaygateSettings.ServiceUrls.TryGetValue(key, out var result))
            {
                throw new NotSupportedException($"Owner {owner} is not supported by FxPaygate");
            }

            return result;
        }

        public string SelectCreditVouchersService()
        {
            return _creditVouchersSettings.ServiceUrls[0];
        }
    }
}
