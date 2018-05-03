namespace Lykke.Service.PaymentSystem.Core.Domain
{
    public interface IPaymentLimits
    {
        double CreditVouchersMinValue { get; set; }
        double CreditVouchersMaxValue { get; set; }
    }
}
