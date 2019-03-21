namespace Lykke.Service.PaymentSystem.Core.Domain
{
    public interface IPaymentLimits
    {
        double CreditVouchersMinValue { get; set; }
        double CreditVouchersMaxValue { get; set; }
        double FxpaygateMinValue { get; set; }
        double FxpaygateMaxValue { get; set; }
        double EasyPaymentGatewayMinValue { get; set; }
        double EasyPaymentGatewayMaxValue { get; set; }
    }
}
