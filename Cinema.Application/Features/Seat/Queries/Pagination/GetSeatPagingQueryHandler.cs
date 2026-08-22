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

namespace Cinema.Application.Features.Seat.Queries.Pagination
{
    public class GetSeatPagingQueryHandler(ISeatRepository SeatRepository , ILogger<GetSeatPagingQueryHandler> logger) 
        : IRequestHandler<GetFoodPagingQuery, ApiResult<PagedResult<Domain.Enitities.Seat>>>
    {
        public async Task<ApiResult<PagedResult<Domain.Enitities.Seat>>> Handle(GetFoodPagingQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin : GetSeatPagingQueryHandler");

                PagedResult<Domain.Enitities.Seat> paging;
                paging = await SeatRepository.GetPagingAsync(request.PageNumber, request.PageSize);
                logger.LogInformation("end: GetSeatPagingQueryHandler");
                return new ApiSuccessResult<PagedResult<Domain.Enitities.Seat>>(paging, "Get Paged success");
            
                     
        }
    }
}

