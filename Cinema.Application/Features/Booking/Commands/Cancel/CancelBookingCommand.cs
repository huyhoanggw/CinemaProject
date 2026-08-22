using MediatR;
using SeedWorks.ApiReponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Booking.Commands.Cancel
{
    public class CancelBookingCommand : IRequest<ApiResult<bool>>
    {
      public Guid bookingId { get; set; }
    }
}
