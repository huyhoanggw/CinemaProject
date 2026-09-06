using Cinema.Application.Interfaces;
using Cinema.Contracts.Reponse;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Showtime.Queries.GetShowtimeById
{
    public class GetShowtimeByIdQueryHandler(IShowtimeRepository ShowtimeRepository , ILogger<GetShowtimeByIdQueryHandler> logger): IRequestHandler<GetShowtimeByIdQuery, ApiResult<Domain.Enitities.Showtime>>
    {
        public async Task<ApiResult<Domain.Enitities.Showtime>> Handle(GetShowtimeByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin:GetShowtimeByIdQueryHandler");
            var Showtime = await ShowtimeRepository.FindByIdAsync(request.Id);
            if (Showtime == null) return new ApiErrorResult<Domain.Enitities.Showtime>("Showtime Id not found");
            logger.LogInformation("end:GetShowtimeByIdQueryHandler");
            return new ApiSuccessResult<Domain.Enitities.Showtime>(Showtime, "Get Showtime Successfully");
        }
    }
}
