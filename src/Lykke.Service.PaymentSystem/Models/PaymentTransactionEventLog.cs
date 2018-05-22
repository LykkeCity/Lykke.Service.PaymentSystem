using System;
using Lykke.Service.PaymentSystem.Core.Domain;

namespace Lykke.Service.PaymentSystem.Models
{
    public class PaymentTransactionEventLog : IPaymentTransactionEventLog
    {
        public string PaymentTransactionId { get; set; }
        public DateTime DateTime { get; set; }
        public string TechData { get; set; }
        public string Message { get; set; }
        public string Who { get; set; }
    }
}
