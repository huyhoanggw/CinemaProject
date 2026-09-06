using Cinema.Application.Features.Services.Payment;
using Cinema.Contracts.Models.Payment;
using Cinema.Contracts.Reponse;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Payment.Command.ReturnUrl
{
    public class ReturnPaymentCommandHandler(IPaymentService services , ILogger<ReturnPaymentCommandHandler> logger) : IRequestHandler<ReturnPaymentCommand, ApiResult<PaymentReturnDto>>
    {
        public async Task<ApiResult<PaymentReturnDto>> Handle(ReturnPaymentCommand request, CancellationToken cancellationToken)
        {
            var result = await services.HandlerPaymentReturn(request.parameters , cancellationToken);
            return result.success ? new Cinema.Contracts.Reponse.ApiSuccessResult<PaymentReturnDto>(result, "") 
                : new Cinema.Contracts.Reponse.ApiErrorResult<PaymentReturnDto>( "Error Occurred while return payment");
        }
    }
}
