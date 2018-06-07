using System;
using Lykke.Service.PaymentSystem.Core.Domain;
using Microsoft.WindowsAzure.Storage.Table;

namespace Lykke.Service.PaymentSystem.AzureRepositories.Entities
{
    public class PaymentTransactionEventLogEntity : TableEntity, IPaymentTransactionEventLog
    {
        public DateTime DateTime { get; set; }
        public string TechData { get; set; }
        public string Message { get; set; }
        public string Who { get; set; }

        public static string GeneratePartitionKey(string transactionId) => transactionId;
        public string PaymentTransactionId => PartitionKey;
    }
}
