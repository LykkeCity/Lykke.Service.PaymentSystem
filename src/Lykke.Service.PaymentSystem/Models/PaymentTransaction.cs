using System;
using Lykke.Contracts.Payments;
using Lykke.Service.PaymentSystem.Core.Domain;

namespace Lykke.Service.PaymentSystem.Models
{
    public class PaymentTransaction : IPaymentTransaction
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
        public double Amount { get; set; }
        public string AssetId { get; set; }
        public string WalletId { get; set; }
        public double? DepositedAmount { get; set; }
        public string DepositedAssetId { get; set; }
        public double? Rate { get; set; }
        public string AggregatorTransactionId { get; set; }
        public DateTime Created { get; set; }
        public PaymentStatus Status { get; set; }
        public CashInPaymentSystem PaymentSystem { get; set; }
        public string Info { get; set; }
        public string OtherData { get; set; }
        public double FeeAmount { get; set; }
        public string MeTransactionId { get; set; }
    }
}
