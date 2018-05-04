using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AzureStorage;
using AzureStorage.Tables.Templates.Index;
using Lykke.Service.PaymentSystem.AzureRepositories.Entities;
using Lykke.Service.PaymentSystem.Core.Domain;
using Lykke.Service.PaymentSystem.Core.Repositories;

namespace Lykke.Service.PaymentSystem.AzureRepositories
{
    public class PaymentTransactionsRepository: IPaymentTransactionsRepository, IRepository
    {
        private const string IndexPartitionKey = "IDX";

        private readonly INoSQLTableStorage<PaymentTransactionEntity> _tableStorage;
        private readonly INoSQLTableStorage<AzureMultiIndex> _tableStorageIndices;

        public PaymentTransactionsRepository(
            INoSQLTableStorage<PaymentTransactionEntity> tableStorage,
            INoSQLTableStorage<AzureMultiIndex> tableStorageIndices)
        {
            _tableStorage = tableStorage;
            _tableStorageIndices = tableStorageIndices;
        }

        public async Task InsertAsync(IPaymentTransaction paymentTransaction)
        {
            var commonEntity = Mapper.Map<PaymentTransactionEntity>(paymentTransaction);
            commonEntity.PartitionKey = PaymentTransactionEntity.GeneratePartitionKey();
            await _tableStorage.InsertAndGenerateRowKeyAsDateTimeAsync(commonEntity, paymentTransaction.Created);

            var entityByClient = Mapper.Map<PaymentTransactionEntity>(paymentTransaction);
            entityByClient.PartitionKey = PaymentTransactionEntity.GeneratePartitionKey(paymentTransaction.ClientId);
            entityByClient.RowKey = PaymentTransactionEntity.GenerateRowKey(paymentTransaction.Id);

            var index = AzureMultiIndex.Create(IndexPartitionKey, paymentTransaction.Id, commonEntity, entityByClient);

            await Task.WhenAll(
                _tableStorage.InsertAsync(entityByClient),
                _tableStorageIndices.InsertAsync(index)
            );
        }

        public async Task<IPaymentTransaction> GetLastByDateAsync(string clientId)
        {
            var partitionKey = PaymentTransactionEntity.GeneratePartitionKey(clientId);
            var entities = await _tableStorage.GetDataAsync(partitionKey);

            return entities.OrderByDescending(itm => itm.Created).FirstOrDefault();
        }
    }
}
