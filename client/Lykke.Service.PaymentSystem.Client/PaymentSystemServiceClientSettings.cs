using System;
using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.PaymentSystem.Client 
{
    public class PaymentSystemServiceClientSettings 
    {
        [HttpCheck("api/isAlive")]
        public string ServiceUrl {get; set;}
    }
}
