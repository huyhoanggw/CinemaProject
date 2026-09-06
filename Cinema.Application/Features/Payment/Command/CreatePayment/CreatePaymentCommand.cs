using Cinema.Contracts.Models.Payment;
using Cinema.Contracts.Reponse;
using Cinema.Domain.Enitities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Payment.Command.CreatePayment
{
    public class CreatePaymentCommand : IRequest<ApiResult<PaymentResult>>
    {
      public  Guid BookingId {  get; set; }
        public PaymentMethod paymentMethod { get; set; }
        public string returnUrl { get; set; }
    }
}
