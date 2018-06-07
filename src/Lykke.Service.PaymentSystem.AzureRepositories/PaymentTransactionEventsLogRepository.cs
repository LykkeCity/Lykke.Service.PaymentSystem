using System.Threading.Tasks;
using AutoMapper;
using AzureStorage;
using Lykke.Service.PaymentSystem.AzureRepositories.Entities;
using Lykke.Service.PaymentSystem.Core.Domain;
using Lykke.Service.PaymentSystem.Core.Repositories;

namespace Lykke.Service.PaymentSystem.AzureRepositories
{
    public class PaymentTransactionEventsLogRepository : IPaymentTransactionEventsLogRepository, IRepository
    {
        private readonly INoSQLTableStorage<PaymentTransactionEventLogEntity> _tableStorage;

        public PaymentTransactionEventsLogRepository(INoSQLTableStorage<PaymentTransactionEventLogEntity> tableStorage)
        {
            _tableStorage = tableStorage;
        }

        public async Task InsertAsync(IPaymentTransactionEventLog newEvent)
        {
            var newEntity = Mapper.Map<PaymentTransactionEventLogEntity>(newEvent);
            await _tableStorage.InsertAndGenerateRowKeyAsDateTimeAsync(newEntity, newEntity.DateTime);
        }
    }
}
