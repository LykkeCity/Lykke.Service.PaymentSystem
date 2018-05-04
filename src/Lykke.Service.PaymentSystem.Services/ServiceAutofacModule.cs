using Autofac;
using AzureStorage;
using AzureStorage.Tables;
using AzureStorage.Tables.Templates.Index;
using Common.Log;
using Lykke.Service.PaymentSystem.AzureRepositories;
using Lykke.Service.PaymentSystem.AzureRepositories.Entities;
using Lykke.Service.PaymentSystem.Core.Settings.ServiceSettings;
using Lykke.SettingsReader;

namespace Lykke.Service.PaymentSystem.Services
{
    public class ServiceAutofacModule : Module
    {
        private readonly IReloadingManager<PaymentSystemSettings> _settings;
        private readonly ILog _log;

        public ServiceAutofacModule(IReloadingManager<PaymentSystemSettings> settings, ILog log)
        {
            _settings = settings;
            _log = log;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(y => AzureTableStorage<PaymentTransactionEntity>.Create(
                    _settings.ConnectionString(x => x.Db.ClientPersonalInfoConnString), "PaymentTransactions", _log))
                .As(typeof(INoSQLTableStorage<PaymentTransactionEntity>));

            builder.Register(y => AzureTableStorage<AzureMultiIndex>.Create(
                    _settings.ConnectionString(x => x.Db.ClientPersonalInfoConnString), "PaymentTransactions", _log))
                .As(typeof(INoSQLTableStorage<AzureMultiIndex>));

            builder.Register(y => AzureTableStorage<PaymentTransactionEventLogEntity>.Create(
                    _settings.ConnectionString(x => x.Db.LogsConnString), "PaymentsLog", _log))
                .As(typeof(INoSQLTableStorage<PaymentTransactionEventLogEntity>));

            builder.Register(y => AzureTableStorage<IdentityEntity>.Create(
                    _settings.ConnectionString(x => x.Db.ClientPersonalInfoConnString), "Setup", _log))
                .As(typeof(INoSQLTableStorage<IdentityEntity>));

            builder.RegisterAssemblyTypes(typeof(IRepository).Assembly)
                .Where(t => typeof(IRepository).IsAssignableFrom(t))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
