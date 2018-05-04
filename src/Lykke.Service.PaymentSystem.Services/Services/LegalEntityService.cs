using System;
using System.Threading.Tasks;
using Lykke.Service.PaymentSystem.Core.Services;
using Lykke.Service.PaymentSystem.Core.Settings.ServiceSettings.PaymentSystem;
using MarginTrading.Backend.Contracts.DataReaderClient;

namespace Lykke.Service.PaymentSystem.Services.Services
{
    public class LegalEntityService : ILegalEntityService, IService
    {
        private readonly IMtDataReaderClient _mtDataReaderClient;
        private readonly string _legalEntity;

        public LegalEntityService(IMtDataReaderClientsPair mtDataReaderClientsPair, PaymentSettings paymentSettings)
        {
            _mtDataReaderClient = mtDataReaderClientsPair.Get(true);
            _legalEntity = paymentSettings.LegalEntity;
        }

        public async Task<string> GetLegalEntityAsync(string walletId)
        {
            if (string.IsNullOrEmpty(walletId))
            {
                return _legalEntity;
            }
            var account = await _mtDataReaderClient.AccountsApi.GetAccountById(walletId);

            if (account == null)
                throw new ArgumentException($"No account found with id {walletId}.");

            if (string.IsNullOrEmpty(account.LegalEntity))
                throw new ArgumentException($"LegalEntity is not set in account with Id {walletId}");

            return account.LegalEntity;
        }
    }
}
