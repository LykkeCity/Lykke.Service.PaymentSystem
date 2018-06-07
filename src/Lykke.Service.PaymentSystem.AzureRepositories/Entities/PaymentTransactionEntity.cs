using System;
using Common;
using Lykke.Contracts.Payments;
using Lykke.Service.PaymentSystem.Core.Domain;
using Microsoft.WindowsAzure.Storage.Table;

namespace Lykke.Service.PaymentSystem.AzureRepositories.Entities
{
    public class PaymentTransactionEntity : TableEntity, IPaymentTransaction
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public string ClientId { get; set; }
        public DateTime Created { get; set; }
        public string Info { get; set; }
        public double? Rate { get; set; }
        public string AggregatorTransactionId { get; set; }
        public double Amount { get; set; }
        public string AssetId { get; set; }
        public string WalletId { get; set; }
        public double? DepositedAmount { get; set; }
        public string DepositedAssetId { get; set; }
        public double FeeAmount { get; set; }
        public string MeTransactionId { get; set; }
        public string Status { get; set; }
        public string PaymentSystem { get; set; }

        string IPaymentTransaction.Id => TransactionId ?? Id.ToString();
        CashInPaymentSystem IPaymentTransaction.PaymentSystem => PaymentSystem.ParseEnum(CashInPaymentSystem.Unknown);
        PaymentStatus IPaymentTransaction.Status => Status.ParseEnum(PaymentStatus.Created);

        public static string GeneratePartitionKey() => "BCO";
        public static string GeneratePartitionKey(string clientId) => clientId;
        public static string GenerateRowKey(string orderId) => orderId;
    }
}
