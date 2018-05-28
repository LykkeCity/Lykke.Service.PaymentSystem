using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.PaymentSystem.Client.AutorestClient;
using Lykke.Service.PaymentSystem.Client.AutorestClient.Models;

namespace Lykke.Service.PaymentSystem.Client
{
    /// <summary>
    /// PaymentSystemClient
    /// </summary>
    public class PaymentSystemClient :  IDisposable
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

            _service = new PaymentSystemAPI(new Uri(serviceUrl));
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
            string okUrl, 
            string failUrl, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _service.PaymentUrlAsync(clientId, amount, assetId, walletId, firstName, lastName,
                city, zip, address, country, email, phone, okUrl, failUrl, cancellationToken);
        }

        /// <summary>
        /// Get last PaymentTransaction by date
        /// </summary>
        /// <param name="clientId">ClientId</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>PaymentTransactionResponse</returns>
        public async Task<PaymentTransactionResponse> GetLastByDateAsync(string clientId = default(string), CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _service.LastAsync(clientId, cancellationToken);
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
    }
}
