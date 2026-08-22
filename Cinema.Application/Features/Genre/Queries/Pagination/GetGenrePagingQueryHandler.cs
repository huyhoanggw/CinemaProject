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

namespace Cinema.Application.Features.Genre.Queries.Pagination
{
    public class GetGenrePagingQueryHandler(IGenreRepository GenreRepository , ILogger<GetGenrePagingQueryHandler> logger) 
        : IRequestHandler<GetGenrePagingQuery, ApiResult<PagedResult<Domain.Enitities.Genre>>>
    {
        public async Task<ApiResult<PagedResult<Domain.Enitities.Genre>>> Handle(GetGenrePagingQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin : GetGenrePagingQueryHandler");
            var paging = await GenreRepository.GetPagingAsync(request.PageNumber , request.PageSize);
               logger.LogInformation("end: GetGenrePagingQueryHandler");
            return new ApiSuccessResult<PagedResult<Domain.Enitities.Genre>>(paging,"Get Paged success");
        }
    }
}
