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

namespace Cinema.Application.Features.Showtime.Queries.Pagination
{
    public class GetTheaterPagingQueryHandler(IShowtimeRepository ShowtimeRepository , ILogger<GetTheaterPagingQueryHandler> logger) 
        : IRequestHandler<GetTheaterPagingQuery, ApiResult<PagedResult<Domain.Enitities.Showtime>>>
    {
        public async Task<ApiResult<PagedResult<Domain.Enitities.Showtime>>> Handle(GetTheaterPagingQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin : GetShowtimePagingQueryHandler");

                PagedResult<Domain.Enitities.Showtime> paging;
            if (!string.IsNullOrEmpty(request.Keywords) && (request.startTime is not null && request.endTime is not null))
            {
                paging = await ShowtimeRepository.GetPagingAsync(request.PageNumber, request.PageSize,
                    x => x.Movie.Name.Contains(request.Keywords),
                    x => x.StartTime == request.startTime && x.EndTime == request.endTime);
            }
            else if (!string.IsNullOrEmpty(request.Keywords))
                paging = await ShowtimeRepository.GetPagingAsync(request.PageNumber, request.PageSize, x => x.Movie.Name.Contains(request.Keywords));
            else if (request.startTime is not null && request.endTime is not null)
                paging = await ShowtimeRepository.GetPagingAsync(request.PageNumber, request.PageSize,
                   x => x.StartTime == request.startTime && x.EndTime == request.endTime);
            else
                paging = await ShowtimeRepository.GetPagingAsync(request.PageNumber, request.PageSize);
                logger.LogInformation("end: GetShowtimePagingQueryHandler");
                return new ApiSuccessResult<PagedResult<Domain.Enitities.Showtime>>(paging, "Get Paged success");
            
                     
        }
    }
}

