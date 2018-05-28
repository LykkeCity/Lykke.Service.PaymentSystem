using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Lykke.Service.PaymentSystem.Client.AutorestClient.Models;

namespace Lykke.Service.PaymentSystem.Client
{
    /// <summary>
    /// PaymentSystemClient
    /// </summary>
    public interface IPaymentSystemClient
    {
        /// <summary>
        /// Get urls for payment
        /// </summary>
        /// <param name="clientId">ClientId</param>
        /// <param name="amount">Amount</param>
        /// <param name="assetId">AssetId</param>
        /// <param name="walletId">WalletId</param>
        /// <param name="firstName">First name</param>
        /// <param name="lastName">Last name</param>
        /// <param name="city">City</param>
        /// <param name="zip">Zip</param>
        /// <param name="address">Address</param>
        /// <param name="country">Country</param>
        /// <param name="email">Email</param>
        /// <param name="phone">Phone</param>
        /// <param name="okUrl">OkUrl</param>
        /// <param name="failUrl">FailUrl</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Url fot payment</returns>
        Task<PaymentUrlDataResponse> GetUrlDataAsync(
            string clientId,
            double? amount,
            string assetId,
            string walletId,
            string firstName,
            string lastName,
            string city,
            string zip,
            string address,
            string country,
            string email,
            string phone,
            string okUrl,
            string failUrl,
            CancellationToken cancellationToken = default(CancellationToken));


        /// <summary>
        /// Get last PaymentTransaction by date
        /// </summary>
        /// <param name="clientId">ClientId</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>PaymentTransactionResponse</returns>
        Task<PaymentTransactionResponse> GetLastByDateAsync(string clientId = default(string), CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get payment limits
        /// </summary>
        /// <returns></returns>
        Task<PaymentLimitsResponse> GetPaymentLimitsAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get PaymentMethods
        /// </summary>
        /// <param name="clientId">ClientId</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>List PaymentMethods</returns>
        Task<PaymentMethodsResponse> GetPaymentMethodsAsync(string clientId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
