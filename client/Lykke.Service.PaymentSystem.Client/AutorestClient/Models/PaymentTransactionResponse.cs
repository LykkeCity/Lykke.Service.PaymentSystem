// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Lykke.Service.PaymentSystem.Client.AutorestClient.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class PaymentTransactionResponse
    {
        /// <summary>
        /// Initializes a new instance of the PaymentTransactionResponse class.
        /// </summary>
        public PaymentTransactionResponse()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the PaymentTransactionResponse class.
        /// </summary>
        /// <param name="status">Possible values include: 'Created',
        /// 'NotifyProcessed', 'NotifyDeclined', 'Processing'</param>
        /// <param name="paymentSystem">Possible values include: 'Unknown',
        /// 'CreditVoucher', 'Bitcoin', 'Ethereum', 'Swift', 'SolarCoin',
        /// 'ChronoBank', 'Fxpaygate', 'Quanta'</param>
        public PaymentTransactionResponse(double amount, System.DateTime created, PaymentStatus status, CashInPaymentSystem paymentSystem, double feeAmount, string id = default(string), string clientId = default(string), string assetId = default(string), string walletId = default(string), double? depositedAmount = default(double?), string depositedAssetId = default(string), double? rate = default(double?), string aggregatorTransactionId = default(string), string info = default(string), string otherData = default(string), string meTransactionId = default(string))
        {
            Id = id;
            ClientId = clientId;
            Amount = amount;
            AssetId = assetId;
            WalletId = walletId;
            DepositedAmount = depositedAmount;
            DepositedAssetId = depositedAssetId;
            Rate = rate;
            AggregatorTransactionId = aggregatorTransactionId;
            Created = created;
            Status = status;
            PaymentSystem = paymentSystem;
            Info = info;
            OtherData = otherData;
            FeeAmount = feeAmount;
            MeTransactionId = meTransactionId;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "ClientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Amount")]
        public double Amount { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "AssetId")]
        public string AssetId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "WalletId")]
        public string WalletId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "DepositedAmount")]
        public double? DepositedAmount { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "DepositedAssetId")]
        public string DepositedAssetId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Rate")]
        public double? Rate { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "AggregatorTransactionId")]
        public string AggregatorTransactionId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Created")]
        public System.DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets possible values include: 'Created', 'NotifyProcessed',
        /// 'NotifyDeclined', 'Processing'
        /// </summary>
        [JsonProperty(PropertyName = "Status")]
        public PaymentStatus Status { get; set; }

        /// <summary>
        /// Gets or sets possible values include: 'Unknown', 'CreditVoucher',
        /// 'Bitcoin', 'Ethereum', 'Swift', 'SolarCoin', 'ChronoBank',
        /// 'Fxpaygate', 'Quanta'
        /// </summary>
        [JsonProperty(PropertyName = "PaymentSystem")]
        public CashInPaymentSystem PaymentSystem { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Info")]
        public string Info { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "OtherData")]
        public string OtherData { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "FeeAmount")]
        public double FeeAmount { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "MeTransactionId")]
        public string MeTransactionId { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
        }
    }
}
