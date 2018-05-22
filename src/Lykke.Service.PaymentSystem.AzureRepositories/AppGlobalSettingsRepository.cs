using System.Threading.Tasks;
using AzureStorage;
using Lykke.Service.PaymentSystem.AzureRepositories.Entities;
using Lykke.Service.PaymentSystem.Core.Domain;
using Lykke.Service.PaymentSystem.Core.Repositories;

namespace Lykke.Service.PaymentSystem.AzureRepositories
{
    public class AppGlobalSettingsRepository : IAppGlobalSettingsRepository, IRepository
    {
        public string GeneratePartitionKey() => "Setup";
        public string GenerateRowKey() => "AppSettings";

        private readonly INoSQLTableStorage<AppGlobalSettingsEntity> _tableStorage;

        public AppGlobalSettingsRepository(INoSQLTableStorage<AppGlobalSettingsEntity> tableStorage)
        {
            _tableStorage = tableStorage;
        }

        public async Task<IAppGlobalSettings> GetAsync()
        {
            var partitionKey = GeneratePartitionKey();
            var rowKey = GenerateRowKey();
            return await _tableStorage.GetDataAsync(partitionKey, rowKey);
        }
    }
}
