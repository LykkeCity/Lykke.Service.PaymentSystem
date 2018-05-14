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
        Task<HttpOperationResponse<IList<PaymentMethodResponse>>> GetPaymentMethodsWithHttpMessagesAsync(string clientId, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <param name='clientId'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<PaymentTransactionResponse>> GetLastByDateWithHttpMessagesAsync(string clientId = default(string), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <param name='clientId'>
        /// </param>
        /// <param name='amount'>
        /// </param>
        /// <param name='assetId'>
        /// </param>
        /// <param name='walletId'>
        /// </param>
        /// <param name='firstName'>
        /// </param>
        /// <param name='lastName'>
        /// </param>
        /// <param name='city'>
        /// </param>
        /// <param name='zip'>
        /// </param>
        /// <param name='address'>
        /// </param>
        /// <param name='country'>
        /// </param>
        /// <param name='email'>
        /// </param>
        /// <param name='phone'>
        /// </param>
        /// <param name='depositOption'>
        /// Possible values include: 'Unknown', 'BankCard', 'Other'
        /// </param>
        /// <param name='okUrl'>
        /// </param>
        /// <param name='failUrl'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<PaymentUrlDataResponse>> PostPaymentUrlDataWithHttpMessagesAsync(string clientId = default(string), double? amount = default(double?), string assetId = default(string), string walletId = default(string), string firstName = default(string), string lastName = default(string), string city = default(string), string zip = default(string), string address = default(string), string country = default(string), string email = default(string), string phone = default(string), DepositOption? depositOption = default(DepositOption?), string okUrl = default(string), string failUrl = default(string), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

    }
}
