using Lykke.Contracts.Payments;
using System;
using System.Runtime.Serialization;

namespace Lykke.Service.PaymentSystem.Core.Exceptions
{
    public class PaymentSystemNotSupportedException : Exception
    {
        public PaymentSystemNotSupportedException()
        {
        }

        public PaymentSystemNotSupportedException(string message, CashInPaymentSystem paymentSystem) : base(message)
        {
            PaymentSystem = paymentSystem;
        }

        public PaymentSystemNotSupportedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PaymentSystemNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public CashInPaymentSystem PaymentSystem { get; }
    }
}
