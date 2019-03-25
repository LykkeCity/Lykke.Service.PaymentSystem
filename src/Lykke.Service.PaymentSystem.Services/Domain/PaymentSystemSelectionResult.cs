using Lykke.Contracts.Payments;

namespace Lykke.Service.PaymentSystem.Services.Domain
{
    public class PaymentSystemSelectionResult
    {
        public CashInPaymentSystem PaymentSystem { get; set; }

        public string ServiceUrl { get; set; }
    }
}
