
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

namespace Cinema.Application.Features.Theater.Queries.GetTheaterById
{
    public class GetSeatByIdQueryHandler(ITheaterRepository TheaterRepository , ILogger<GetSeatByIdQueryHandler> logger): IRequestHandler<GetTheaterByIdQuery, ApiResult<Domain.Enitities.Theater>>
    {
        public async Task<ApiResult<Domain.Enitities.Theater>> Handle(GetTheaterByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin: GetTheaterByIdQueryHandler");
            var Theater = await TheaterRepository.FindByIdAsync(request.Id);
            if (Theater == null) return new ApiErrorResult<Domain.Enitities.Theater>("Theater Id not found");
            logger.LogInformation("end: GetTheaterByIdQueryHandler");
            return new ApiSuccessResult<Domain.Enitities.Theater>(Theater, "Get Theater Successfully");
        }
    }
}
