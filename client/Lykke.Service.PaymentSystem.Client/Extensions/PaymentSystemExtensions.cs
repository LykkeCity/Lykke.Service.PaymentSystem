using System;
using Lykke.Service.PaymentSystem.Client.AutorestClient.Models;

namespace Lykke.Service.PaymentSystem.Client.Extensions
{
    /// <summary>
    /// PaymentSystem extensions
    /// </summary>
    public static class PaymentSystemExtensions
    {
        /// <summary>
        /// GetInfo as T
        /// </summary>
        /// <typeparam name="T">Return type</typeparam>
        /// <param name="src">PaymentTransactionResponse object</param>
        /// <returns>Deserialize object as T</returns>
        public static T GetInfo<T>(this PaymentTransactionResponse src)
        {
            if (!PaymentSystemsAndOtherInfo.PsAndOtherInfoLinks.ContainsKey(src.PaymentSystem))
            {
                throw new Exception("Unsupported payment system for reading other info: transactionId:" + src.Id);
            }

            var type = PaymentSystemsAndOtherInfo.PsAndOtherInfoLinks[src.PaymentSystem];

            if (type != typeof(T))
            {
                throw new Exception("Payment system and Other info does not match for transactionId:" + src.Id);
            }

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(src.Info);
        }
    }
}
