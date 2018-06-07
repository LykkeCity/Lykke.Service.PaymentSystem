using System.Threading.Tasks;
using Lykke.Service.PaymentSystem.Core.Repositories;
using Lykke.Service.PaymentSystem.Core.Services;

namespace Lykke.Service.PaymentSystem.Services.Services
{
    public class AppGlobalSettingsService : IAppGlobalSettingsService, IService
    {
        private readonly IAppGlobalSettingsRepository _appGlobalSettingsRepository;
        public AppGlobalSettingsService(IAppGlobalSettingsRepository appGlobalSettingsRepository)
        {
            _appGlobalSettingsRepository = appGlobalSettingsRepository;
        }

        public async Task<bool> IsOnMaintenanceAsync()
        {
            var appGlobalSettings = await _appGlobalSettingsRepository.GetAsync();
            return appGlobalSettings.IsOnMaintenance;
        }
    }
}
