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

namespace Cinema.Application.Features.Genre.Queries.GetGenreById
{
    public class GetGenreByIdQueryHandler(IGenreRepository genreRepository , ILogger<GetGenreByIdQueryHandler> logger) : IRequestHandler<GetGenreByIdQuery, ApiResult<Domain.Enitities.Genre>>
    {
        public async Task<ApiResult<Domain.Enitities.Genre>> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin:GetGenreByIdQueryHandler");
            var genre = await genreRepository.FindByIdAsync(request.Id);
            if(genre is null) return new ApiErrorResult<Domain.Enitities.Genre>("Genre Id not found");
            logger.LogInformation("end:GetGenreByIdQueryHandler");
            return new ApiSuccessResult<Domain.Enitities.Genre>(genre, "Get genre successfuly");
        }
    }
}
