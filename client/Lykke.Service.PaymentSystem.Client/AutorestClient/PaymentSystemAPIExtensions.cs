// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Lykke.Service.PaymentSystem.Client.AutorestClient
{
    using Models;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for PaymentSystemAPI.
    /// </summary>
    public static partial class PaymentSystemAPIExtensions
    {
            /// <summary>
            /// Checks service is alive
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static object IsAlive(this IPaymentSystemAPI operations)
            {
                return operations.IsAliveAsync().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Checks service is alive
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> IsAliveAsync(this IPaymentSystemAPI operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.IsAliveWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static PaymentLimitsResponse GetPaymentLimits(this IPaymentSystemAPI operations)
            {
                return operations.GetPaymentLimitsAsync().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<PaymentLimitsResponse> GetPaymentLimitsAsync(this IPaymentSystemAPI operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetPaymentLimitsWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            public static PaymentMethodsResponse GetPaymentMethods(this IPaymentSystemAPI operations, string clientId)
            {
                return operations.GetPaymentMethodsAsync(clientId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<PaymentMethodsResponse> GetPaymentMethodsAsync(this IPaymentSystemAPI operations, string clientId, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetPaymentMethodsWithHttpMessagesAsync(clientId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            public static PaymentTransactionResponse GetLastByDate(this IPaymentSystemAPI operations, string clientId = default(string))
            {
                return operations.GetLastByDateAsync(clientId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<PaymentTransactionResponse> GetLastByDateAsync(this IPaymentSystemAPI operations, string clientId = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetLastByDateWithHttpMessagesAsync(clientId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
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
            /// <param name='cancelUrl'>
            /// </param>
            public static PaymentUrlDataResponse PostPaymentUrlData(this IPaymentSystemAPI operations, string clientId = default(string), double? amount = default(double?), string assetId = default(string), string walletId = default(string), string firstName = default(string), string lastName = default(string), string city = default(string), string zip = default(string), string address = default(string), string country = default(string), string email = default(string), string phone = default(string), DepositOption? depositOption = default(DepositOption?), string okUrl = default(string), string failUrl = default(string), string cancelUrl = default(string))
            {
                return operations.PostPaymentUrlDataAsync(clientId, amount, assetId, walletId, firstName, lastName, city, zip, address, country, email, phone, depositOption, okUrl, failUrl, cancelUrl).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
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
            /// <param name='cancelUrl'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<PaymentUrlDataResponse> PostPaymentUrlDataAsync(this IPaymentSystemAPI operations, string clientId = default(string), double? amount = default(double?), string assetId = default(string), string walletId = default(string), string firstName = default(string), string lastName = default(string), string city = default(string), string zip = default(string), string address = default(string), string country = default(string), string email = default(string), string phone = default(string), DepositOption? depositOption = default(DepositOption?), string okUrl = default(string), string failUrl = default(string), string cancelUrl = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.PostPaymentUrlDataWithHttpMessagesAsync(clientId, amount, assetId, walletId, firstName, lastName, city, zip, address, country, email, phone, depositOption, okUrl, failUrl, cancelUrl, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
