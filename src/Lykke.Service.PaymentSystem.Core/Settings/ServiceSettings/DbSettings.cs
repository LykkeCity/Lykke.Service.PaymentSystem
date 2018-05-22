using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.PaymentSystem.Core.Settings.ServiceSettings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }
        [AzureTableCheck]
        public string ClientPersonalInfoConnString { get; set; }
    }
}
