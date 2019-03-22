using System.Net;
using System.Threading.Tasks;
using Lykke.Contracts.Payments;
using Lykke.Service.ClientAccount.Client;
using Lykke.Service.PaymentSystem.Core.Services;
using Lykke.Service.PaymentSystem.Core.Settings.ServiceSettings.PaymentSystem;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using Lykke.Service.PaymentSystem.Models;

namespace Lykke.Service.PaymentSystem.Controllers
{
    [Route("api/[controller]")]
    public class PaymentMethodsController : Controller
    {
        private readonly IClientAccountClient _clientAccount;
        private readonly IAppGlobalSettingsService _appGlobalSettingsService;
        private readonly PaymentSettings _paymentSettings;

        public PaymentMethodsController(
            IClientAccountClient clientAccount,
            IAppGlobalSettingsService appGlobalSettingsService,
            PaymentSettings paymentSettings)
        {
            _clientAccount = clientAccount;
            _appGlobalSettingsService = appGlobalSettingsService;
            _paymentSettings = paymentSettings;
        }

        [HttpPost("{clientId}")]
        [SwaggerOperation("GetPaymentMethods")]
        [ProducesResponseType(typeof(PaymentMethodsResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(string clientId)
        {
            var depositViaCreditCardBlocked = (await _clientAccount.GetDepositBlockAsync(clientId)).DepositViaCreditCardBlocked;
            var isOnMaintenance = await _appGlobalSettingsService.IsOnMaintenanceAsync();

            var result = new PaymentMethodsResponse
            {                     
                PaymentMethods = new[]    
                {
                    new PaymentMethod
                    {
                        Name = CashInPaymentSystem.Fxpaygate.ToString(),
                        Assets = _paymentSettings.Fxpaygate.SupportedCurrencies,
                        Available = !depositViaCreditCardBlocked && !isOnMaintenance
                    },
                    new PaymentMethod
                    {
                        Name = CashInPaymentSystem.CreditVoucher.ToString(),
                        Assets = _paymentSettings.CreditVouchers.SupportedCurrencies,
                        Available = !isOnMaintenance
                    },
                    new PaymentMethod
                    {
                        Name = CashInPaymentSystem.EasyPaymentGateway.ToString(),
                        Assets = _paymentSettings.EasyPaymentGateway.SupportedCurrencies,
                        Available = !depositViaCreditCardBlocked && !isOnMaintenance
                    }
                }
            };

            return Ok(result);
        }
    }
}
