using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Log;
using Lykke.Service.Assets.Client;
using Lykke.Service.ClientAccount.Client;
using Lykke.Service.FeeCalculator.Client;
using Lykke.Service.PaymentSystem.AzureRepositories;
using Lykke.Service.PaymentSystem.Core.Repositories;
using Lykke.Service.PaymentSystem.Core.Services;
using Lykke.Service.PaymentSystem.Core.Settings.ServiceSettings;
using Lykke.Service.PaymentSystem.Core.Settings.ServiceSettings.PaymentSystem;
using Lykke.Service.PaymentSystem.Services;
using Lykke.Service.PaymentSystem.Services.Services;
using Lykke.Service.PersonalData.Client;
using Lykke.Service.PersonalData.Contract;
using Lykke.SettingsReader;
using MarginTrading.Backend.Contracts.DataReaderClient;
using Microsoft.Extensions.DependencyInjection;

namespace Lykke.Service.PaymentSystem.Modules
{
    public class ApiAutofacModule : Module
    {
        private readonly IReloadingManager<PaymentSystemSettings> _settings;
        private readonly ILog _log;

        public ApiAutofacModule(IReloadingManager<PaymentSystemSettings> settings, ILog log)
        {
            _settings = settings;
            _log = log;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_log)
                .As<ILog>()
                .SingleInstance();

            builder.RegisterType<HealthService>()
                .As<IHealthService>()
                .SingleInstance();

            builder.RegisterType<StartupManager>()
                .As<IStartupManager>();

            builder.RegisterType<ShutdownManager>()
                .As<IShutdownManager>();

            builder.RegisterInstance(_settings.CurrentValue.PaymentSettings)
                .As<PaymentSettings>()
                .SingleInstance();

            builder.RegisterAssemblyTypes(typeof(IService).Assembly)
                .Where(t => typeof(IService).IsAssignableFrom(t))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterInstance(_settings.CurrentValue.PaymentSettings);

            IServiceCollection services = new ServiceCollection();

            services.RegisterMtDataReaderClientsPair(
                _settings.CurrentValue.MarginSettings.DataReaderDemoApiUrl,
                _settings.CurrentValue.MarginSettings.DataReaderLiveApiUrl,
                _settings.CurrentValue.MarginSettings.DataReaderDemoApiKey,
                _settings.CurrentValue.MarginSettings.DataReaderLiveApiKey,
                "Lykke.Service.PaymentSystem");

            builder.RegisterLykkeServiceClient(_settings.CurrentValue.ClientAccountServiceClient.ServiceUrl);
            builder.RegisterInstance<IAssetsService>(
                new AssetsService(new Uri(_settings.CurrentValue.AssetsServices.ServiceUrl)));

            builder.RegisterType<PersonalDataService>().As<IPersonalDataService>()
                .WithParameter(TypedParameter.From(_settings.CurrentValue.PersonalDataServiceSettings));

            builder.RegisterFeeCalculatorClient(_settings.CurrentValue.FeeCalculatorServiceClient.ServiceUrl, _log);

            builder.Populate(services);

            base.Load(builder);
        }
    }
}
