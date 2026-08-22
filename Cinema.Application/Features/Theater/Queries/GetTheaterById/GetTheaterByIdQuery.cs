using MediatR;
using SeedWorks.ApiReponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Theater.Queries.GetTheaterById
{
    public class GetTheaterByIdQuery : IRequest<ApiResult<Domain.Enitities.Theater>>
    {
        public Guid Id { get; set; }
    }
}

