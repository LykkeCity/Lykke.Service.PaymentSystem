using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.PaymentSystem.Client.AutorestClient;
using Lykke.Service.PaymentSystem.Client.AutorestClient.Models;
using Lykke.Service.PaymentSystem.Client.Extensions;

namespace Lykke.Service.PaymentSystem.Client
{
    /// <summary>
    /// PaymentSystem extensions
    /// </summary>
    public static class PaymentSystemExtensions
    {
        /// <summary>
        /// GetInfo as object
        /// </summary>
        /// <param name="src"></param>
        /// <param name="expectedType"></param>
        /// <param name="throwExeption"></param>
        /// <returns></returns>
        public static object GetInfo(this PaymentTransactionResponse src, Type expectedType = null, bool throwExeption = false)
        {
            if (!PaymentSystemsAndOtherInfo.PsAndOtherInfoLinks.ContainsKey(src.PaymentSystem))
            {
                if (throwExeption)
                    throw new Exception("Unsupported payment system for reading other info: transactionId:" + src.Id);

                return null;
            }

            var type = PaymentSystemsAndOtherInfo.PsAndOtherInfoLinks[src.PaymentSystem];

            if (expectedType != null)
            {
                if (type != expectedType)
                    throw new Exception("Payment system and Other info does not match for transactionId:" + src.Id);
            }

            return Newtonsoft.Json.JsonConvert.DeserializeObject(src.Info, type);
        }

        /// <summary>
        /// GetInfo as T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        /// <returns></returns>
        public static T GetInfo<T>(this PaymentTransactionResponse src)
        {
            return (T)GetInfo(src, typeof(T), true);
        }
    }
}
