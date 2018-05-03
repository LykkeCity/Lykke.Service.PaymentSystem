using System;
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
        Task<PaymentUrlDataResponse> GetUrlDataAsync(
            string clientPaymentSystem,
            string orderId,
            string clientId,
            double amount,
            string assetId,
            string walletId,
            string isoCountryCode,
            string otherInfoJson,
            CancellationToken cancellationToken = default(CancellationToken)
            );

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
        Task InsertPaymentTransactionAsync(double amount, PaymentStatus status, CashInPaymentSystem paymentSystem, double feeAmount, string id = default(string), string clientId = default(string), string assetId = default(string), string depositedAssetId = default(string), string walletId = default(string), string info = default(string), double? depositedAmount = default(double?), double? rate = default(double?), string aggregatorTransactionId = default(string), string otherData = default(string), string meTransactionId = default(string), CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get last PaymentTransaction by date
        /// </summary>
        /// <param name="clientId">ClientId</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>PaymentTransactionResponse</returns>
        Task<PaymentTransactionResponse> GetLastByDateAsync(string clientId = default(string), CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Insert PaymentTransactionEventLog
        /// </summary>
        /// <param name="paymentTransactionId">PaymentTransactionId</param>
        /// <param name="techData">Technical data</param>
        /// <param name="message">Message</param>
        /// <param name="who">Who</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        Task InsertPaymentTransactionEventLogAsync(string paymentTransactionId = default(string), string techData = default(string), string message = default(string), string who = default(string), CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get payment limits
        /// </summary>
        /// <returns></returns>
        Task<PaymentLimitsResponse> GetPaymentLimitsAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
