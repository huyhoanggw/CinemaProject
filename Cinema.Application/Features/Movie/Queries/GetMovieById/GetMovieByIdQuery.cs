using Cinema.Contracts.Reponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Movie.Queries.GetMovieById
{
    public class GetMovieByIdQuery : IRequest<ApiResult<Domain.Enitities.Movie>>
    {
        public Guid Id { get; set; }
    }
}
