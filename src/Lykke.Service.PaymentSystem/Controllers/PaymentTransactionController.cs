using System.Net;
using System.Threading.Tasks;
using Lykke.Contracts.Payments;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using Lykke.Service.PaymentSystem.Core.Services;
using Lykke.Service.PaymentSystem.Models;
using Lykke.Service.PersonalData.Contract;

namespace Lykke.Service.PaymentSystem.Controllers
{
    [Route("api/[controller]")]
    public class PaymentTransactionController : Controller
    {
        private readonly IPaymentTransactionsService _paymentTransactionsService;
        private readonly IPersonalDataService _personalDataService;

        public PaymentTransactionController(
            IPaymentTransactionsService paymentTransactionsService, 
            IPersonalDataService personalDataService)
        {
            _paymentTransactionsService = paymentTransactionsService;
            _personalDataService = personalDataService;
        }

        [HttpGet]
        [SwaggerOperation("GetLastByDate")]
        [ProducesResponseType(typeof(PaymentTransactionResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLastByDate(string clientId)
        {
            var lastPaymentTransaction = await _paymentTransactionsService.GetLastByDateAsync(clientId);

            var personalData = await _personalDataService.GetAsync(clientId);

            if (personalData == null)
            {
                return BadRequest("A user with such an clientId does not exist.");
            }

            var isCreditVoucherOrFxpaygate = lastPaymentTransaction != null
                    && (lastPaymentTransaction.PaymentSystem == CashInPaymentSystem.CreditVoucher
                        || lastPaymentTransaction.PaymentSystem == CashInPaymentSystem.Fxpaygate);

            var result = isCreditVoucherOrFxpaygate
                ? PaymentTransactionResponse.Create(lastPaymentTransaction, personalData)
                : PaymentTransactionResponse.Create(personalData);

            return Ok(result);
        }
    }
}
