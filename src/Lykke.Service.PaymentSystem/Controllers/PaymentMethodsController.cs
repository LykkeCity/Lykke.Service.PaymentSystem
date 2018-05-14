using System.Net;
using System.Threading.Tasks;
using Common.Log;
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
        private readonly ILog _log;

        public PaymentMethodsController(
            ILog log,
            IClientAccountClient clientAccount,
            IAppGlobalSettingsService appGlobalSettingsService,
            PaymentSettings paymentSettings)
        {
            _log = log;
            _clientAccount = clientAccount;
            _appGlobalSettingsService = appGlobalSettingsService;
            _paymentSettings = paymentSettings;
        }

        [HttpPost("{clientId}")]
        [SwaggerOperation("GetPaymentMethods")]
        [ProducesResponseType(typeof(PaymentMethodResponse[]), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(string clientId)
        {
            var depositViaCreditCardBlocked = (await _clientAccount.GetDepositBlockAsync(clientId)).DepositViaCreditCardBlocked;
            var isOnMaintenance = await _appGlobalSettingsService.IsOnMaintenanceAsync();

            var result = new[]
            {
                new PaymentMethodResponse
                {
                    Name = CashInPaymentSystem.Fxpaygate.ToString(),
                    Assets = _paymentSettings.Fxpaygate.SupportedCurrencies,
                    Available = !depositViaCreditCardBlocked && !isOnMaintenance
                },
                new PaymentMethodResponse
                {
                    Name = CashInPaymentSystem.CreditVoucher.ToString(),
                    Assets = _paymentSettings.CreditVouchers.SupportedCurrencies,
                    Available = !isOnMaintenance
                }
            };

            return Ok(result);
        }
    }
}
