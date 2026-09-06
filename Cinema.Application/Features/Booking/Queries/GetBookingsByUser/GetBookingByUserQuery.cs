using Cinema.Contracts.Reponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Booking.Queries.GetBookingsByUser
{
    public class GetBookingByUserQuery : IRequest<ApiResult<List<Domain.Enitities.Booking>>>
    {
        public Guid UserId { get; set; }
    }
}
