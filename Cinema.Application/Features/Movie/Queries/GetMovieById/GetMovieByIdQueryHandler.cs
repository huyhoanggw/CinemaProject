using Cinema.Application.Features.Movie.Queries.GetMovieById;
using Cinema.Application.Features.Showtime.Queries.GetMovieById;
using Cinema.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using SeedWorks.ApiReponse;
using SeedWorks.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Movie.Queries.GetMovieById
{
    public class GetMovieByIdQueryHandler(IMovieRepository MovieRepository , ILogger<GetMovieByIdQueryHandler> logger): IRequestHandler<GetMovieByIdQuery, ApiResult<Domain.Enitities.Movie>>
    {
        public async Task<ApiResult<Domain.Enitities.Movie>> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin: GetMovieByIdQueryHandler");
            var Movie = await MovieRepository.FindByIdAsync(request.Id);
            if (Movie == null) return new ApiErrorResult<Domain.Enitities.Movie>("Movie Id not found");
            
            logger.LogInformation("end: GetMovieByIdQueryHandler");
            return new ApiSuccessResult<Domain.Enitities.Movie>(Movie, "Get Movie Successfully");
        }
    }
}

