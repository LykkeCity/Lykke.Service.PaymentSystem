using System;
using System.Threading.Tasks;
using Lykke.Service.PaymentSystem.Core.Services;
using MarginTrading.Backend.Contracts.DataReaderClient;

namespace Lykke.Service.PaymentSystem.Services.Services
{
    public class LegalEntityService : ILegalEntityService
    {
        private readonly IMtDataReaderClient _mtDataReaderClient;

        public LegalEntityService(IMtDataReaderClientsPair mtDataReaderClientsPair)
        {
            _mtDataReaderClient = mtDataReaderClientsPair.Get(true);
        }

        public async Task<string> GetLegalEntityAsync(string walletId)
        {
            const string legalEntity = "LYKKEVU";

            if (!string.IsNullOrEmpty(walletId))
            {
                var account = await _mtDataReaderClient.AccountsApi.GetAccountById(walletId);

                if (account == null)
                    throw new Exception($"No account found with id {walletId}.");

                if (string.IsNullOrEmpty(account.LegalEntity))
                    throw new Exception($"LegalEntity is not set in account with Id {walletId}");

                return account.LegalEntity;
            }

            return legalEntity;
        }
    }
}
