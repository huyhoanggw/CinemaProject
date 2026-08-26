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

namespace Cinema.Application.Features.Booking.Queries.Pagination
{
    public class GetBookingPagingQueryHandler(IBookingRepository bookingRepository , ILogger<GetBookingPagingQueryHandler> logger) 
        : IRequestHandler<GetBookingPagingQuery, ApiResult<PagedResult<Domain.Enitities.Booking>>>
    {
        public async Task<ApiResult<PagedResult<Domain.Enitities.Booking>>> Handle(GetBookingPagingQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin : GetBookingPagingQueryHandler");
            var paging = await bookingRepository.GetPagingAsync(request.PageNumber , request.PageSize);
               logger.LogInformation("end: GetBookingPagingQueryHandler");
            return new ApiSuccessResult<PagedResult<Domain.Enitities.Booking>>(paging,"Get Paged success");
        }
    }
}
