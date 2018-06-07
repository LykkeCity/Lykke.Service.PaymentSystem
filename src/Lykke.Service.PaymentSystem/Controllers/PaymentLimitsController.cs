using System;
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
    public class PaymentLimitsController : Controller
    {
        private readonly IPaymentLimitsService _paymentLimitsService;

        public PaymentLimitsController(IPaymentLimitsService paymentLimitsService)
        {
            _paymentLimitsService = paymentLimitsService;
        }

        [HttpGet]
        [SwaggerOperation("GetPaymentLimits")]
        [ProducesResponseType(typeof(PaymentLimitsResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var lastPaymentTransaction = await _paymentLimitsService.GetPaymentLimitsAsync();
            var result = Mapper.Map<PaymentLimitsResponse>(lastPaymentTransaction);
            return Ok(result);
        }
    }
}
