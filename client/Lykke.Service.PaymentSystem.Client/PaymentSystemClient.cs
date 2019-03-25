using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Lykke.Service.PaymentSystem.Client.AutorestClient;
using Lykke.Service.PaymentSystem.Client.AutorestClient.Models;

namespace Lykke.Service.PaymentSystem.Client
{
    /// <summary>
    /// PaymentSystemClient
    /// </summary>
    public class PaymentSystemClient : IPaymentSystemClient, IDisposable
    {
        private PaymentSystemAPI _service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceUrl">Service URL</param>
        public PaymentSystemClient(string serviceUrl)
        {
            if (string.IsNullOrWhiteSpace(serviceUrl))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(serviceUrl));

            _service = new PaymentSystemAPI(new Uri(serviceUrl), new HttpClient());
        }

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
        /// <param name="depositOption">Deposit option</param>
        /// <param name="okUrl">OkUrl</param>
        /// <param name="failUrl">FailUrl</param>
        /// <param name="depositOptionEnum">DepositOptionEnum</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<PaymentUrlDataResponse> GetUrlDataAsync(
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
            DepositOption depositOption, 
            string okUrl, 
            string failUrl, 
            string cancelUrl, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _service.PostPaymentUrlDataAsync(amount ?? 0, depositOption, clientId, assetId, walletId, firstName, lastName, city, zip, address, country, email, phone, okUrl, failUrl, cancelUrl, cancellationToken);
        }

        /// <summary>
        /// Get last PaymentTransaction by date
        /// </summary>
        /// <param name="clientId">ClientId</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>PaymentTransactionResponse</returns>
        public async Task<PaymentTransactionResponse> GetLastByDateAsync(string clientId = default(string), CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _service.GetLastByDateAsync(clientId, cancellationToken);
        }

        /// <summary>
        /// Get payment limits
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<PaymentLimitsResponse> GetPaymentLimitsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _service.GetPaymentLimitsAsync(cancellationToken);
        }

        /// <summary>
        /// Get PaymentMethods
        /// </summary>
        /// <param name="clientId">ClientId</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>List PaymentMethods</returns>
        public async Task<PaymentMethodsResponse> GetPaymentMethodsAsync(string clientId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _service.GetPaymentMethodsAsync(clientId, cancellationToken);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Inner dispose
        /// </summary>
        /// <param name="disposing">disposing flag</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && _service != null)
            {
                _service.Dispose();
                _service = null;
            }
        }

        /// <summary>
        /// Get source client id
        /// </summary>
        /// <param name="walletId">The wallet id</param>
        /// <param name="clientPaymentSystem">The client payment system name</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<SourceClientInfoResponse> GetSourceClientIdAsync(string walletId, string clientPaymentSystem, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _service.GetSourceClientIdAsync(walletId, clientPaymentSystem, cancellationToken);

            if (response is ErrorResponse error)
            {
                throw new Exception(error.ErrorMessage);
            }

            if (response is SourceClientInfoResponse result)
            {
                return result;
            }

            throw new Exception("Unexpected API response");
        }
    }
}
