using System.Threading.Tasks;
using Lykke.Service.PaymentSystem.Core.Domain;
using Lykke.Service.PaymentSystem.Core.Repositories;
using Lykke.Service.PaymentSystem.Core.Services;

namespace Lykke.Service.PaymentSystem.Services.Services
{
    public class PaymentTransactionsService : IPaymentTransactionsService
    {
        private readonly IPaymentTransactionsRepository _paymentTransactionsRepository;

        public PaymentTransactionsService(IPaymentTransactionsRepository paymentTransactionsRepository)
        {
            _paymentTransactionsRepository = paymentTransactionsRepository;
        }

        public async Task<IPaymentTransaction> GetLastByDateAsync(string clientId)
        {
            return await _paymentTransactionsRepository.GetLastByDateAsync(clientId);
        }

        public async Task InsertPaymentTransactionAsync(IPaymentTransaction model)
        {
            await _paymentTransactionsRepository.InsertAsync(model);
        }
    }
}
