using MediatR;
using MediatR.Pipeline;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Payment.Command.ReturnUrl
{
    public class ReturnPaymentCommand : IRequest<ApiResult<PaymentReturnDto>>
    {
        public SortedDictionary<string, string> parameters = new();
    }
}
