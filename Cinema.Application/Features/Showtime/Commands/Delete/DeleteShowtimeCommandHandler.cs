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

namespace Cinema.Application.Features.Showtime.Commands.Delete
{
    public class DeleteShowtimeCommandHandler(IShowtimeRepository showtimeRepository, IUnitOfWork unitOfWork, ILogger<DeleteShowtimeCommandHandler> logger) : IRequestHandler<DeleteShowtimeCommand, ApiResult<bool>>
    {
        public async Task<ApiResult<bool>> Handle(DeleteShowtimeCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin:DeleteShowtimeCommandHandler");
            var showtime = await showtimeRepository.FindByIdAsync(request.ShowtimeId);
            if (showtime is null) return new ApiErrorResult<bool>("Showtime Id not found");
            await showtimeRepository.DeleteAsync(showtime.Id);
            var result = await unitOfWork.SaveChangeAsync(cancellationToken);
            logger.LogInformation("end:DeleteShowtimeCommandHandler");

            return result > 0 ? new ApiSuccessResult<bool>("Delete showtime successfully ") : new ApiErrorResult<bool>("Error occurred while delete showtime");

        }
    }
}
