using Cinema.Application.Features.Services.PaymentService;
using Cinema.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Payment.Command.CreatePayment
{
    public class CreatePaymentCommandHandler(IPaymentService paymentService ,IUnitOfWork unitofWork , IHttpContextAccessor httpContext , ILogger<CreatePaymentCommandHandler> logger)
        : IRequestHandler<CreatePaymentCommand, ApiResult<PaymentResult>>
    {
        public async Task<ApiResult<PaymentResult>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var clientIp = httpContext.HttpContext?.Connection.RemoteIpAddress?.ToString();
            var result = await paymentService.CreatePaymentAsync(request.BookingId , request.paymentMethod 
                , request.returnUrl , clientIp ?? "127.0.0.1", cancellationToken);
            return new SeedWorks.Reponse.ApiSuccessResult<PaymentResult>(result, "create payment success");
        }
    }
}
