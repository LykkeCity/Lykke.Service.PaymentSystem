using Lykke.Service.Assets.Client;
using Lykke.Service.ClientAccount.Client;
using Lykke.Service.FeeCalculator.Client;
using Lykke.Service.PaymentSystem.Core.Settings.ServiceSettings;
using Lykke.Service.PaymentSystem.Core.Settings.ServiceSettings.PaymentSystem;
using Lykke.Service.PaymentSystem.Core.Settings.SlackNotifications;
using Lykke.Service.PersonalData.Settings;

namespace Lykke.Service.PaymentSystem.Core.Settings
{
    public class AppSettings
    {
        public PaymentSystemSettings PaymentSystemService { get; set; }
        public SlackNotificationsSettings SlackNotifications { get; set; }
        public AssetsServiceSettingsCustom AssetsServiceClient { get; set; }
        public ClientAccountServiceClientSettings ClientAccountServiceClient { get; set; }
        public FeeCalculatorServiceClientSettings FeeCalculatorServiceClient { get; set; }
        public PersonalDataServiceClientSettings PersonalDataServiceClient { get; set; }
    }
}
