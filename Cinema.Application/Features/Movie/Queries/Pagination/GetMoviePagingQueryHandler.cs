using Cinema.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Pagination;
using SeedWorks.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Movie.Queries.Pagination
{
    public class GetMoviePagingQueryHandler(IMovieRepository MovieRepository , ILogger<GetMoviePagingQueryHandler> logger) 
        : IRequestHandler<GetMoviePagingQuery, ApiResult<PagedResult<Domain.Enitities.Movie>>>
    {
        public async Task<ApiResult<PagedResult<Domain.Enitities.Movie>>> Handle(GetMoviePagingQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin : GetMoviePagingQueryHandler");
            
            if (!string.IsNullOrEmpty(request.Keywords))
            {
                var paging = await MovieRepository.GetPagingAsync(request.PageNumber, request.PageSize ,x=> x.Name.Contains(request.Keywords));
                logger.LogInformation("end: GetMoviePagingQueryHandler");
                return new ApiSuccessResult<PagedResult<Domain.Enitities.Movie>>(paging, "Get Paged success");
            }
            else
            {
                var paging = await MovieRepository.GetPagingAsync(request.PageNumber, request.PageSize);
                logger.LogInformation("end: GetMoviePagingQueryHandler");
                return new ApiSuccessResult<PagedResult<Domain.Enitities.Movie>>(paging, "Get Paged success");
            }
          
        }
    }
}

