using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.PaymentSystem.Core.Settings.ServiceSettings.PaymentSystem
{
    public class AssetsServiceSettingsCustom
    {
        [HttpCheck("api/isAlive")]
        public string ServiceUrl { set; get; }
    }
}
