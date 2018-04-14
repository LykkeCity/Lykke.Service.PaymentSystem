using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Log;
using Lykke.Service.PaymentSystem.Core.Settings.ServiceSettings;
using Lykke.Service.PaymentSystem.Modules;
using Lykke.Service.PaymentSystem.Services;
using Lykke.SettingsReader;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lykke.Service.PaymentSystem
{
    public class AutofacConfiguration
    {
        public static ContainerBuilder Register(IServiceCollection services, IReloadingManager<PaymentSystemSettings> configuration, ILog log)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new ApiAutofacModule(configuration, log));
            builder.RegisterModule(new ServiceAutofacModule(configuration, log));

            builder.Populate(services);

            return builder;
        }
    }
}
