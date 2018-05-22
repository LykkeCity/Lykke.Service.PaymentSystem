using System.Threading.Tasks;
using Lykke.Service.PaymentSystem.Core.Domain;

namespace Lykke.Service.PaymentSystem.Core.Services
{
    public interface IPaymentTransactionsService
    {
        Task<IPaymentTransaction> GetLastByDateAsync(string clientId);
        Task InsertPaymentTransactionAsync(IPaymentTransaction model);
    }
}
