using Lykke.Service.PaymentSystem.Core.Settings.ServiceSettings;
using Lykke.Service.PaymentSystem.Core.Settings.SlackNotifications;

namespace Lykke.Service.PaymentSystem.Core.Settings
{
    public class AppSettings
    {
        public PaymentSystemSettings PaymentSystemService { get; set; }
        public SlackNotificationsSettings SlackNotifications { get; set; }
    }
}
