using MediatR;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Payment.Command.CallbackPayment
{
    public class CallbackPaymentCommand : IRequest<ApiResult<bool>>
    {
        public SortedDictionary<string, string> Parameters { get; set; } = new();
    }
}
