using System.Threading.Tasks;
using Lykke.Service.PaymentSystem.Core.Domain;
using Lykke.Service.PaymentSystem.Core.Repositories;
using Lykke.Service.PaymentSystem.Core.Services;

namespace Lykke.Service.PaymentSystem.Services.Services
{
    public class PaymentTransactionEventLogService : IPaymentTransactionEventLogService
    {
        private readonly IPaymentTransactionEventsLogRepository _paymentTransactionEventsLogRepository;

        public PaymentTransactionEventLogService(IPaymentTransactionEventsLogRepository paymentTransactionEventsLogRepository)
        {
            _paymentTransactionEventsLogRepository = paymentTransactionEventsLogRepository;
        }

        public async Task InsertPaymentTransactionEventLogAsync(IPaymentTransactionEventLog newEvent)
        {
            await _paymentTransactionEventsLogRepository.InsertAsync(newEvent);
        }
    }
}
