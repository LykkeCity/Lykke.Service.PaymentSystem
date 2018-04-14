using System.Threading.Tasks;

namespace Lykke.Service.PaymentSystem.Core.Services
{
    public interface ILegalEntityService
    {
        Task<string> GetLegalEntityAsync(string walletId);
    }
}
