using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Log;
using Lykke.Service.PaymentSystem.Core.Settings;
using Lykke.Service.PaymentSystem.Core.Settings.ServiceSettings;
using Lykke.Service.PaymentSystem.Modules;
using Lykke.Service.PaymentSystem.Services;
using Lykke.SettingsReader;
using Microsoft.Extensions.DependencyInjection;

namespace Lykke.Service.PaymentSystem
{
    public static class AutofacConfiguration
    {
        public static ContainerBuilder Register(IServiceCollection services, AppSettings appSettings, IReloadingManager<PaymentSystemSettings> configuration, ILog log)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new ApiAutofacModule(appSettings, configuration, log));
            builder.RegisterModule(new ServiceAutofacModule(configuration, log));

            builder.Populate(services);

            return builder;
        }
    }
}
