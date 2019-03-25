using System.Threading.Tasks;
using Lykke.Service.PaymentSystem.Core.Domain.PaymentUrlData;

namespace Lykke.Service.PaymentSystem.Core.Services
{
    public interface IPaymentUrlDataService
    {
        Task<PaymentUrlData> GetUrlDataAsync(
            string paymentSystem, 
            string transactionId, 
            string clientId, 
            double fullAmount, 
            string assetId, 
            string walletId, 
            string countryIso3Code, 
            string info);

        Task<string> GenerateNewTransactionIdAsync();

        Task<string> GetSourceClientIdAsync(string walletId, string clientPaymentSystem);
    }
}
