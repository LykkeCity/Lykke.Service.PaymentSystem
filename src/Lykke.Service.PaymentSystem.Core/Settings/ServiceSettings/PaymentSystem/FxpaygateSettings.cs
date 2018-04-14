using System.Collections.Generic;

namespace Lykke.Service.PaymentSystem.Core.Settings.ServiceSettings.PaymentSystem
{
    public class FxpaygateSettings
    {
        public string[] Currencies { get; set; }
        public string[] Countries { get; set; }
        public Dictionary<string, string> ServiceUrls { get; set; }
        public string[] SupportedCurrencies { get; set; }
    }
}