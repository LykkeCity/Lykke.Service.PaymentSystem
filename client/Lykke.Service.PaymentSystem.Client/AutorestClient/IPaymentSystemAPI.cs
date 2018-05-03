// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Lykke.Service.PaymentSystem.Client.AutorestClient
{
    using Microsoft.Rest;
    using Models;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// </summary>
    public partial interface IPaymentSystemAPI : System.IDisposable
    {
        /// <summary>
        /// The base URI of the service.
        /// </summary>
        System.Uri BaseUri { get; set; }

        /// <summary>
        /// Gets or sets json serialization settings.
        /// </summary>
        JsonSerializerSettings SerializationSettings { get; }

        /// <summary>
        /// Gets or sets json deserialization settings.
        /// </summary>
        JsonSerializerSettings DeserializationSettings { get; }


        /// <summary>
        /// Checks service is alive
        /// </summary>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<object>> IsAliveWithHttpMessagesAsync(Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<PaymentLimitsResponse>> GetPaymentLimitsWithHttpMessagesAsync(Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <param name='clientId'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<PaymentTransactionResponse>> GetLastByDateWithHttpMessagesAsync(string clientId = default(string), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <param name='amount'>
        /// </param>
        /// <param name='created'>
        /// </param>
        /// <param name='status'>
        /// Possible values include: 'Created', 'NotifyProcessed',
        /// 'NotifyDeclined', 'Processing'
        /// </param>
        /// <param name='paymentSystem'>
        /// Possible values include: 'Unknown', 'CreditVoucher', 'Bitcoin',
        /// 'Ethereum', 'Swift', 'SolarCoin', 'ChronoBank', 'Fxpaygate',
        /// 'Quanta'
        /// </param>
        /// <param name='feeAmount'>
        /// </param>
        /// <param name='id'>
        /// </param>
        /// <param name='clientId'>
        /// </param>
        /// <param name='assetId'>
        /// </param>
        /// <param name='walletId'>
        /// </param>
        /// <param name='depositedAmount'>
        /// </param>
        /// <param name='depositedAssetId'>
        /// </param>
        /// <param name='rate'>
        /// </param>
        /// <param name='aggregatorTransactionId'>
        /// </param>
        /// <param name='info'>
        /// </param>
        /// <param name='otherData'>
        /// </param>
        /// <param name='meTransactionId'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> PostPaymentTransactionWithHttpMessagesAsync(double amount, System.DateTime created, PaymentStatus status, CashInPaymentSystem paymentSystem, double feeAmount, string id = default(string), string clientId = default(string), string assetId = default(string), string walletId = default(string), double? depositedAmount = default(double?), string depositedAssetId = default(string), double? rate = default(double?), string aggregatorTransactionId = default(string), string info = default(string), string otherData = default(string), string meTransactionId = default(string), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <param name='dateTime'>
        /// </param>
        /// <param name='paymentTransactionId'>
        /// </param>
        /// <param name='techData'>
        /// </param>
        /// <param name='message'>
        /// </param>
        /// <param name='who'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> PostPaymentTransactionEventsLogWithHttpMessagesAsync(System.DateTime dateTime, string paymentTransactionId = default(string), string techData = default(string), string message = default(string), string who = default(string), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <param name='amount'>
        /// </param>
        /// <param name='clientPaymentSystem'>
        /// </param>
        /// <param name='orderId'>
        /// </param>
        /// <param name='clientId'>
        /// </param>
        /// <param name='assetId'>
        /// </param>
        /// <param name='walletId'>
        /// </param>
        /// <param name='isoCountryCode'>
        /// </param>
        /// <param name='otherInfoJson'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<PaymentUrlDataResponse>> PostPaymentUrlDataWithHttpMessagesAsync(double amount, string clientPaymentSystem = default(string), string orderId = default(string), string clientId = default(string), string assetId = default(string), string walletId = default(string), string isoCountryCode = default(string), string otherInfoJson = default(string), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

    }
}
