using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Lykke.Contracts.Payments;

namespace Lykke.Service.PaymentSystem.Core.Extensions
{
    /// <summary>
    /// PaymentSystemsAndOtherInfo
    /// </summary>
    public static class PaymentSystemsAndOtherInfo
    {
        /// <summary>
        /// PaymentSystem And other info links
        /// </summary>
        public static readonly ImmutableDictionary<CashInPaymentSystem, Type> PsAndOtherInfoLinks = new Dictionary<CashInPaymentSystem, Type>
        {
            [CashInPaymentSystem.CreditVoucher] = typeof(OtherPaymentInfo),
            [CashInPaymentSystem.Fxpaygate] = typeof(OtherPaymentInfo),
            [CashInPaymentSystem.EasyPaymentGateway] = typeof(OtherPaymentInfo)
        }
        .ToImmutableDictionary();
    }
}
