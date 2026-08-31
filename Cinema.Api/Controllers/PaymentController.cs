using Cinema.Application.Features.Payment.Command.CallbackPayment;
using Cinema.Application.Features.Payment.Command.CreatePayment;
using Cinema.Application.Features.Payment.Command.ReturnUrl;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Api.Controllers
{
    public class PaymentController(IMediator mediar , ILogger<PaymentController> logger) :BaseController 
    {
        [HttpPost()]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentCommand request)
        {
               var result = await mediar.Send(request);
            return result.IsSuccess ? Ok(result) : NotFound();
        }
        [HttpGet("vnp/ipn")]
        public async Task<IActionResult> VnpIpn()
        {
            var sortDictionary = new SortedDictionary<string, string>(Request.Query.ToDictionary(x => x.Key, x => x.Value.ToString()));
            var command = new CallbackPaymentCommand()
            {
                Parameters = sortDictionary
            };
               var result = await mediar.Send(command);
            return result.IsSuccess ? Ok(result) : NotFound();
        }
        [HttpGet("vnp/return_url")]
        public async Task<IActionResult> vnpReturn()
        {
            var sortDictionary = new SortedDictionary<string ,string>(Request.Query.ToDictionary(x=> x.Key , x => x.Value.ToString())) ;
            var result = await mediar.Send(new ReturnPaymentCommand() { parameters = sortDictionary });
            return Ok(result);
        }
    }
}
