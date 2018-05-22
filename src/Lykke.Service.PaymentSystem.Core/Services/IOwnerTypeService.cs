using System.Threading.Tasks;
using Lykke.Service.ClientAccount.Client.AutorestClient.Models;

namespace Lykke.Service.PaymentSystem.Core.Services
{
    public interface IOwnerTypeService
    {
        Task<OwnerType> GetOwnerTypeAsync(string walletId);
    }
}
