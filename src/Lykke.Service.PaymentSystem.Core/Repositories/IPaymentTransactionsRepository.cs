using System.Threading.Tasks;
using Lykke.Service.PaymentSystem.Core.Domain;

namespace Lykke.Service.PaymentSystem.Core.Repositories
{
    public interface IPaymentTransactionsRepository
    {
        Task InsertAsync(IPaymentTransaction paymentTransaction);
        Task<IPaymentTransaction> GetLastByDateAsync(string clientId);
    }
}
