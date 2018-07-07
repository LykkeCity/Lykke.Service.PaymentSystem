// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Lykke.Service.PaymentSystem.Client.AutorestClient.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class PaymentUrlDataResponse
    {
        /// <summary>
        /// Initializes a new instance of the PaymentUrlDataResponse class.
        /// </summary>
        public PaymentUrlDataResponse()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the PaymentUrlDataResponse class.
        /// </summary>
        public PaymentUrlDataResponse(string url = default(string), string okUrl = default(string), string failUrl = default(string), string cancelUrl = default(string))
        {
            Url = url;
            OkUrl = okUrl;
            FailUrl = failUrl;
            CancelUrl = cancelUrl;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Url")]
        public string Url { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "OkUrl")]
        public string OkUrl { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "FailUrl")]
        public string FailUrl { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "CancelUrl")]
        public string CancelUrl { get; set; }

    }
}
