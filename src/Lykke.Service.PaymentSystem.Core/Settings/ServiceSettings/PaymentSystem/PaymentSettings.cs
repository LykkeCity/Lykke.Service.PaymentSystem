namespace Lykke.Service.PaymentSystem.Core.Settings.ServiceSettings.PaymentSystem
{
    public class PaymentSettings
    {
        public CreditVouchersSettings CreditVouchers { get; set; }
        public FxpaygateSettings Fxpaygate { get; set; }
        public EasyPaymentGatewaySettings EasyPaymentGateway { get; set; }
        public string LegalEntity { get; set; }
    }
}
