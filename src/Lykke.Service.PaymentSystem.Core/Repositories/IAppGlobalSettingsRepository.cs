using System.Threading.Tasks;
using Lykke.Service.PaymentSystem.Core.Domain;

namespace Lykke.Service.PaymentSystem.Core.Repositories
{
    public interface IAppGlobalSettingsRepository
    {
        Task<IAppGlobalSettings> GetAsync();
    }
}
