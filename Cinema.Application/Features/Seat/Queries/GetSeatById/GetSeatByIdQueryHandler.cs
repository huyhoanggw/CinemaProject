
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

namespace Cinema.Application.Features.Seat.Queries.GetSeatById
{
    public class GetSeatByIdQueryHandler(ISeatRepository SeatRepository , ILogger<GetSeatByIdQueryHandler> logger): IRequestHandler<GetSeatByIdQuery, ApiResult<Domain.Enitities.Seat>>
    {
        public async Task<ApiResult<Domain.Enitities.Seat>> Handle(GetSeatByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin: GetSeatByIdQueryHandler");
            var Seat = await SeatRepository.FindByIdAsync(request.Id);
            if (Seat == null) return new ApiErrorResult<Domain.Enitities.Seat>("Seat Id not found");
            logger.LogInformation("end: GetSeatByIdQueryHandler");
            return new ApiSuccessResult<Domain.Enitities.Seat>(Seat, "Get Seat Successfully");
        }
    }
}
