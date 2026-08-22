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

namespace Cinema.Application.Features.Theater.Queries.Pagination
{
    public class GetTheaterPagingQueryHandler(ITheaterRepository TheaterRepository , ILogger<GetTheaterPagingQueryHandler> logger) 
        : IRequestHandler<GetFoodPagingQuery, ApiResult<PagedResult<Domain.Enitities.Theater>>>
    {
        public async Task<ApiResult<PagedResult<Domain.Enitities.Theater>>> Handle(GetFoodPagingQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin : GetTheaterPagingQueryHandler");

                PagedResult<Domain.Enitities.Theater> paging;
                paging = await TheaterRepository.GetPagingAsync(request.PageNumber, request.PageSize);
                logger.LogInformation("end: GetTheaterPagingQueryHandler");
                return new ApiSuccessResult<PagedResult<Domain.Enitities.Theater>>(paging, "Get Paged success");
            
                     
        }
    }
}

