﻿
using Lykke.Service.PaymentSystem.Core.Domain;

namespace Lykke.Service.PaymentSystem.Services.Domain
{
    public class PaymentLimits : IPaymentLimits
    {
        public double CreditVouchersMinValue { get; set; }
        public double CreditVouchersMaxValue { get; set; }
        public double FxpaygateMinValue { get; set; }
        public double FxpaygateMaxValue { get; set; }
    }
}
