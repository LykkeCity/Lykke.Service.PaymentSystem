using Lykke.Contracts.Payments;
using Lykke.Service.ClientAccount.Client.AutorestClient.Models;

namespace Lykke.Service.PaymentSystem.Core.Services
{
    public interface IPaymentUrlDataService
    {
        bool IsPaymentSystemSupported(CashInPaymentSystem paymentSystem, string assetId);
        string SelectFxpaygateService(OwnerType owner, string legalEntity = null);
        string SelectCreditVouchersService();
        bool IsFxpaygateAndSpot(string clientPaymentSystem, string assetId, string isoCountryCode);
    }
}
