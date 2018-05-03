using System.Threading.Tasks;
using Lykke.Service.PaymentSystem.Core.Domain;
using Lykke.Service.PaymentSystem.Core.Services;
using Lykke.Service.PaymentSystem.Core.Settings.ServiceSettings.PaymentSystem;
using Lykke.Service.PaymentSystem.Services.Domain;

namespace Lykke.Service.PaymentSystem.Services.Services
{
    public class PaymentLimitsService : IPaymentLimitsService, IService
    {
        private readonly CreditVouchersSettings _creditVouchersSettings;
        public PaymentLimitsService(PaymentSettings paymentSettings)
        {
            _creditVouchersSettings = paymentSettings.CreditVouchers;
        }

        public Task<IPaymentLimits> GetPaymentLimitsAsync()
        {
            return Task.FromResult<IPaymentLimits>(new PaymentLimits
            {
                CreditVouchersMaxValue = _creditVouchersSettings.MaxAmount,
                CreditVouchersMinValue = _creditVouchersSettings.MinAmount
            });
        }
    }
}
