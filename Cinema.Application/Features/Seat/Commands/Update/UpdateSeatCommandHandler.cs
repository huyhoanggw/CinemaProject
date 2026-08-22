using AutoMapper;
using Cinema.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Seat;
using SeedWorks.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Seat.Commands.Update
{
    public class UpdateSeatCommandHandler(ISeatRepository SeatRepository , IMapper mapper , ILogger<UpdateSeatCommandHandler> logger , IUnitOfWork unitOfWork )
        : IRequestHandler<UpdateSeatCommand, ApiResult<Domain.Enitities.Seat>>
    {
        public async Task<ApiResult<Domain.Enitities.Seat>> Handle( UpdateSeatCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin:UpdateSeatCommandHandler");
            var Seat = await SeatRepository.FindByIdAsync(request.Id );
            if (Seat is null) return new ApiErrorResult<Domain.Enitities.Seat> ("Seat Id Not found");
            Seat.Row = request.Row;
            Seat.Number = request.Number;
            var result = await unitOfWork.SaveChangeAsync(cancellationToken);
            logger.LogInformation("end:UpdateSeatCommandHandler");
                        return result >= 0 ? new ApiSuccessResult<Domain.Enitities.Seat>(Seat, "update Seat successfully") : new ApiErrorResult<Domain.Enitities.Seat> ("Error occurred while update Seat");
        }
    }
}
