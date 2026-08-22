using MediatR;
using SeedWorks.ApiReponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Seat.Queries.GetSeatById
{
    public class GetSeatByIdQuery : IRequest<ApiResult<Domain.Enitities.Seat>>
    {
        public Guid Id { get; set; }
    }
}

