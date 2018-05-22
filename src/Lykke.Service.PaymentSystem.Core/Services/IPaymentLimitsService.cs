using System.Threading.Tasks;
using Lykke.Service.PaymentSystem.Core.Domain;

namespace Lykke.Service.PaymentSystem.Core.Services
{
    public interface IPaymentLimitsService
    {
        Task<IPaymentLimits> GetPaymentLimitsAsync();
    }
}
