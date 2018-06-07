using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.PaymentSystem.Core.Domain;
using Lykke.Service.PaymentSystem.Core.Repositories;
using Lykke.Service.PaymentSystem.Core.Services;

namespace Lykke.Service.PaymentSystem.Services.Services
{
    public class PaymentTransactionEventLogService : IPaymentTransactionEventLogService, IService
    {
        private readonly IPaymentTransactionEventsLogRepository _paymentTransactionEventsLogRepository;
        private readonly ILog _log;

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
