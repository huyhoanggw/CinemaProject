using MediatR;
using SeedWorks.ApiReponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Booking.Queries.GetBookingById
{
    public class GetBookingByIdQuery : IRequest<ApiResult<Domain.Enitities.Booking>>
    {
        public Guid Id { get; set; }
    }
}
