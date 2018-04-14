using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using Lykke.Service.PaymentSystem.Core.Services;
using Lykke.Service.PaymentSystem.Models;

namespace Lykke.Service.PaymentSystem.Controllers
{
    [Route("api/[controller]")]
    public class PaymentTransactionEventsLogController : Controller
    {
        private readonly IPaymentTransactionEventLogService _paymentTransactionEventLogService;

        public PaymentTransactionEventsLogController(IPaymentTransactionEventLogService paymentTransactionEventLogService)
        {
            _paymentTransactionEventLogService = paymentTransactionEventLogService;
        }

        [HttpPost]
        [SwaggerOperation("PostPaymentTransactionEventsLog")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Post(PaymentTransactionEventLogRequest model)
        {
            await _paymentTransactionEventLogService.InsertPaymentTransactionEventLogAsync(model);
            return NoContent();
        }
    }
}
