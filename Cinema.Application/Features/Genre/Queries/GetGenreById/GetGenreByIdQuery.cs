using MediatR;
using SeedWorks.ApiReponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Genre.Queries.GetGenreById
{
    public class GetGenreByIdQuery : IRequest<ApiResult<Domain.Enitities.Genre>>
    {
        public Guid Id { get; set; }
    }
}
