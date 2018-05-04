using System;

namespace Lykke.Service.PaymentSystem.Client.Extensions
{
    /// <summary>
    /// Incorrect CashInPaymentSystem
    /// </summary>
    public class BadPaymentSystemException: Exception
    {
        /// <summary>
        /// Exception with message
        /// </summary>
        /// <param name="message">Error message</param>
        public BadPaymentSystemException(string message) : base(message)
        {
        }
    }
}
