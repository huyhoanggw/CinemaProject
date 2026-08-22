using MediatR;
using SeedWorks.ApiReponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Booking.Queries.GetBookingByUserIdAndBookingId
{
    public class GetBookingByUserIdAndBookingIdQuery : IRequest<ApiResult<Cinema.Domain.Enitities.Booking>>
    {
        public string UserId { get; set; }
        public string BookingId { get; set; }
    }
}
