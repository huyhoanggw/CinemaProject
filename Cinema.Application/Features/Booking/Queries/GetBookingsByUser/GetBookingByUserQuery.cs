using MediatR;
using SeedWorks.ApiReponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Booking.Queries.GetBookingByUser
{
    public class GetBookingByUserQuery : IRequest<ApiResult<List<Cinema.Domain.Enitities.Booking>>>
    {
        public Guid UserId { get; set; }
    }
}
