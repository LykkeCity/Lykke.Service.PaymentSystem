// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Lykke.Service.PaymentSystem.Client.AutorestClient.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Runtime;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines values for DepositOption.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DepositOption
    {
        [EnumMember(Value = "Unknown")]
        Unknown,
        [EnumMember(Value = "BankCard")]
        BankCard,
        [EnumMember(Value = "Other")]
        Other
    }
    internal static class DepositOptionEnumExtension
    {
        internal static string ToSerializedValue(this DepositOption? value)
        {
            return value == null ? null : ((DepositOption)value).ToSerializedValue();
        }

        internal static string ToSerializedValue(this DepositOption value)
        {
            switch( value )
            {
                case DepositOption.Unknown:
                    return "Unknown";
                case DepositOption.BankCard:
                    return "BankCard";
                case DepositOption.Other:
                    return "Other";
            }
            return null;
        }

        internal static DepositOption? ParseDepositOption(this string value)
        {
            switch( value )
            {
                case "Unknown":
                    return DepositOption.Unknown;
                case "BankCard":
                    return DepositOption.BankCard;
                case "Other":
                    return DepositOption.Other;
            }
            return null;
        }
    }
}