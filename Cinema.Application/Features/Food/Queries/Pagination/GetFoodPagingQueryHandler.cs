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

namespace Cinema.Application.Features.Food.Queries.Pagination
{
    public class GetFoodPagingQueryHandler(IFoodRepository FoodRepository , ILogger<GetFoodPagingQueryHandler> logger) 
        : IRequestHandler<GetFoodPagingQuery, ApiResult<PagedResult<Domain.Enitities.Food>>>
    {
        public async Task<ApiResult<PagedResult<Domain.Enitities.Food>>> Handle(GetFoodPagingQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin : GetFoodPagingQueryHandler");

                PagedResult<Domain.Enitities.Food> paging;
                paging = await FoodRepository.GetPagingAsync(request.PageNumber, request.PageSize);
                logger.LogInformation("end: GetFoodPagingQueryHandler");
                return new ApiSuccessResult<PagedResult<Domain.Enitities.Food>>(paging, "Get Paged success");
            
                     
        }
    }
}

