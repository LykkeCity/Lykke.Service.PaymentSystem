// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Lykke.Service.PaymentSystem.Client.AutorestClient.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class PaymentMethod
    {
        /// <summary>
        /// Initializes a new instance of the PaymentMethod class.
        /// </summary>
        public PaymentMethod()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the PaymentMethod class.
        /// </summary>
        public PaymentMethod(string name, IList<string> assets, bool available)
        {
            Name = name;
            Assets = assets;
            Available = available;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Assets")]
        public IList<string> Assets { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Available")]
        public bool Available { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Name == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Name");
            }
            if (Assets == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Assets");
            }
        }
    }
}
