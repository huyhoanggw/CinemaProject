using Cinema.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Movie.Queries.GetAll
{
    public class GetMoviesQueryHandler(IMovieRepository repository, ILogger<GetMoviesQueryHandler> logger) : IRequestHandler<GetMoviesQuery, List<Domain.Enitities.Movie>>
    {
        public async Task<List<Domain.Enitities.Movie>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin : GetMoviesQueryHandler");
            var movies = await repository.GetAll();
            return movies.ToList();
        }
    }
}
