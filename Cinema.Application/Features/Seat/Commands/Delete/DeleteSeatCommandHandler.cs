using Cinema.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using SeedWorks.ApiReponse;
using SeedWorks.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Seat.Commands.Delete
{
    public class DeleteSeatCommandHandler(ISeatRepository SeatRepsitory , IUnitOfWork unitOfWork,ILogger<DeleteSeatCommandHandler> logger) : IRequestHandler<DeleteSeatCommand, ApiResult<bool>>
    {
        public async Task<ApiResult<bool>> Handle(DeleteSeatCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin: DeleteSeatCommandHandler");
            var Seat = await SeatRepsitory.FindByIdAsync(request.Id);
            if (Seat is null) return new ApiErrorResult<bool>("Seat Id not found");
            await SeatRepsitory.DeleteAsync(request.Id);
            var result = await unitOfWork.SaveChangeAsync(cancellationToken);
            logger.LogInformation("end: DeleteSeatCommandHandler");
            return result > 0 ? new ApiSuccessResult<bool>("Delete Seat successfully") : new ApiErrorResult<bool>("Error occurred while delete Seat");
        }
    }
}
