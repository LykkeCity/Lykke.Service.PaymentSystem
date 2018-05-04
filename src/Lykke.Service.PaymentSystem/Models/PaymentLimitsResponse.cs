using Lykke.Service.PaymentSystem.Core.Domain;

namespace Lykke.Service.PaymentSystem.Models
{
    public class PaymentLimitsResponse: IPaymentLimits
    {
        public double CreditVouchersMinValue { get; set; }
        public double CreditVouchersMaxValue { get; set; }
    }
}
