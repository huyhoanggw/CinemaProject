using Cinema.Application.Features.Services.PaymentService;
using MediatR;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Payment;
using SeedWorks.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Payment.Command.CallbackPayment
{
    public class CallbackPaymentCommandHandler(IPaymentService services) : IRequestHandler<CallbackPaymentCommand, ApiResult<bool>>
    {
        public async Task<ApiResult<bool>> Handle(CallbackPaymentCommand request, CancellationToken cancellationToken)
        {
             
                var success = await services.HandlerPaymentCallback(request.Parameters, cancellationToken);
            return success ? new ApiSuccessResult<bool>("payment success") : new ApiErrorResult<bool>("payment failed");
        }
    }
}
