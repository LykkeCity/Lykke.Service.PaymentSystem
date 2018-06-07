using System.Threading.Tasks;
using AzureStorage;
using Lykke.Service.PaymentSystem.AzureRepositories.Entities;
using Lykke.Service.PaymentSystem.Core.Repositories;

namespace Lykke.Service.PaymentSystem.AzureRepositories
{
    public class IdentityRepository : IIdentityRepository, IRepository
    {
        private readonly INoSQLTableStorage<IdentityEntity> _tableStorage;

        public IdentityRepository(INoSQLTableStorage<IdentityEntity> tableStorage)
        {
            _tableStorage = tableStorage;
        }

        public async Task<int> GenerateNewIdAsync()
        {
            var result = 0;
            await
                _tableStorage.InsertOrModifyAsync(
                    IdentityEntity.GeneratePartitionKey,
                    IdentityEntity.GenerateRowKey,
                    IdentityEntity.Create, indEnt =>
                    {
                        result = ++indEnt.Value;
                        return true;
                    }
                );
            return result;
        }
    }
}
