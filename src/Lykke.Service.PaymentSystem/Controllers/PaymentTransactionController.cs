using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using Lykke.Service.PaymentSystem.Core.Services;
using Lykke.Service.PaymentSystem.Models;

namespace Lykke.Service.PaymentSystem.Controllers
{
    [Route("api/[controller]")]
    public class PaymentTransactionController : Controller
    {
        private readonly IPaymentTransactionsService _paymentTransactionsService;

        public PaymentTransactionController(IPaymentTransactionsService paymentTransactionsService)
        {
            _paymentTransactionsService = paymentTransactionsService;
        }

        [HttpGet]
        [SwaggerOperation("GetLastByDate")]
        [ProducesResponseType(typeof(PaymentTransactionResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLastByDate(string clientId)
        {
            var lastPaymentTransaction = await _paymentTransactionsService.GetLastByDateAsync(clientId);
            var result = Mapper.Map<PaymentTransactionResponse>(lastPaymentTransaction);
            return Ok(result);
        }

        [HttpPost]
        [SwaggerOperation("PostPaymentTransaction")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Post(PaymentTransactionRequest model)
        {
            await _paymentTransactionsService.InsertPaymentTransactionAsync(model);
            return NoContent();
        }
    }
}
