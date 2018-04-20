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
    public class PaymentSystemClient : IPaymentSystemClient, IDisposable
    {
        private readonly ILog _log;
        private PaymentSystemAPI _service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceUrl"></param>
        /// <param name="log"></param>
        public PaymentSystemClient(string serviceUrl, ILog log)
        {
            if (string.IsNullOrWhiteSpace(serviceUrl))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(serviceUrl));

            _service = new PaymentSystemAPI(new Uri(serviceUrl));
            _log = log;
        }

        /// <summary>
        /// Get UrlData
        /// </summary>
        /// <param name="clientPaymentSystem">Client PaymentSystem</param>
        /// <param name="orderId">OrderId</param>
        /// <param name="clientId">ClientId</param>
        /// <param name="amount">Amount</param>
        /// <param name="assetId">AssetId</param>
        /// <param name="walletId">WalletId</param>
        /// <param name="isoCountryCode">IsoCountryCode</param>
        /// <param name="otherInfoJson">Other info as Json</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>PaymentUrlDataResponse</returns>
        public async Task<PaymentUrlDataResponse> GetUrlDataAsync(
            string clientPaymentSystem,
            string orderId,
            string clientId,
            double amount,
            string assetId,
            string walletId,
            string isoCountryCode,
            string otherInfoJson,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _service.PostPaymentUrlDataAsync(
                amount,
                clientPaymentSystem,
                orderId,
                clientId,
                assetId,
                walletId,
                isoCountryCode,
                otherInfoJson,
                cancellationToken);
        }

        /// <summary>
        /// Insert PaymentTransaction
        /// </summary>
        /// <param name="amount">Amount</param>
        /// <param name="status">PaymentStatus</param>
        /// <param name="paymentSystem">CashInPaymentSystem</param>
        /// <param name="feeAmount">Fee Amount</param>
        /// <param name="id">Id</param>
        /// <param name="clientId">ClientId</param>
        /// <param name="assetId">AssetId</param>
        /// <param name="walletId">WalletId</param>
        /// <param name="depositedAmount">DepositedAmount</param>
        /// <param name="depositedAssetId"></param>
        /// <param name="rate">Rate</param>
        /// <param name="aggregatorTransactionId"></param>
        /// <param name="info">Other payment info as Json</param>
        /// <param name="otherData">Other data</param>
        /// <param name="meTransactionId">MeTransactionId</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task InsertPaymentTransactionAsync(
            double amount, 
            PaymentStatus status, 
            CashInPaymentSystem paymentSystem, 
            double feeAmount, 
            string id = default(string), 
            string clientId = default(string), 
            string assetId = default(string), 
            string depositedAssetId = default(string), 
            string walletId = default(string), 
            string info = default(string), 
            double? depositedAmount = default(double?), 
            double? rate = default(double?), 
            string aggregatorTransactionId = default(string), 
            string otherData = default(string), 
            string meTransactionId = default(string), 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await _service.PostPaymentTransactionAsync(
                amount,
                DateTime.UtcNow, 
                (PaymentStatus)Enum.Parse(typeof(PaymentStatus), status.ToString()),
                (CashInPaymentSystem)Enum.Parse(typeof(CashInPaymentSystem), paymentSystem.ToString()),
                feeAmount,
                id,
                clientId,
                assetId,
                walletId,
                depositedAmount,
                depositedAssetId,
                rate,
                aggregatorTransactionId,
                info,
                otherData,
                meTransactionId,
                cancellationToken);
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
        /// Insert PaymentTransactionEventLog
        /// </summary>
        /// <param name="paymentTransactionId">PaymentTransactionId</param>
        /// <param name="techData">Technical data</param>
        /// <param name="message">Message</param>
        /// <param name="who">Who</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task InsertPaymentTransactionEventLogAsync(
            string paymentTransactionId = default(string),
            string techData = default(string),
            string message = default(string),
            string who = default(string),
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await _service.PostPaymentTransactionEventsLogAsync(
                DateTime.UtcNow,
                paymentTransactionId,
                techData,
                message,
                who,
                cancellationToken);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (_service == null)
                return;
            _service.Dispose();
            _service = null;
        }
    }
}
