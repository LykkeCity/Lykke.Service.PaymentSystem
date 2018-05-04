using System.Threading.Tasks;

namespace Lykke.Service.PaymentSystem.Core.Repositories
{
    public interface IIdentityRepository
    {
        Task<int> GenerateNewIdAsync();
    }
}
