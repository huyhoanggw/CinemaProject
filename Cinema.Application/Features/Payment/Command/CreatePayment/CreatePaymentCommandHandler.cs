using Cinema.Application.Features.Services.Payment;
using Cinema.Application.Interfaces;
using Cinema.Contracts.Models.Payment;
using Cinema.Contracts.Reponse;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
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
            return new Cinema.Contracts.Reponse.ApiSuccessResult<PaymentResult>(result, "create payment success");
        }
    }
}
