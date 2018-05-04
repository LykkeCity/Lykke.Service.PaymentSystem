using System.Threading.Tasks;
using Lykke.Contracts.Payments;
using Lykke.Service.ClientAccount.Client.AutorestClient.Models;
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
    }
}
