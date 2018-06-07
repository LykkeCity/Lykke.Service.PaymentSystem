using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.PaymentSystem.Core.Settings.SlackNotifications
{
    public class AzureQueuePublicationSettings
    {
        [AzureQueueCheck]
        public string ConnectionString { get; set; }

        public string QueueName { get; set; }
    }
}
