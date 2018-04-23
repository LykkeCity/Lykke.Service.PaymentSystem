using System;
using System.Threading.Tasks;
using Lykke.Service.ClientAccount.Client;
using Lykke.Service.ClientAccount.Client.AutorestClient.Models;
using Lykke.Service.PaymentSystem.Core.Services;

namespace Lykke.Service.PaymentSystem.Services.Services
{
    public class OwnerTypeService : IOwnerTypeService, IService
    {
        private readonly IClientAccountClient _clientAccountClient;

        public OwnerTypeService(IClientAccountClient clientAccountClient)
        {
            _clientAccountClient = clientAccountClient;
        }

        public async Task<OwnerType> GetOwnerTypeAsync(string walletId)
        {
            var wallet = await _clientAccountClient.GetWalletAsync(walletId);

            if (wallet == null)
                throw new Exception($"Wallet with ID {walletId} was not found");

            if (!Enum.TryParse(wallet.Owner, out OwnerType owner))
            {
                throw new Exception($"Owner {walletId} is not supported");
            }

            return owner;
        }
    }
}
