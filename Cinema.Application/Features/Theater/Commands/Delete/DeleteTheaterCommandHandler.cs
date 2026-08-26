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

namespace Cinema.Application.Features.Theater.Commands.Delete
{
    public class DeleteTheaterCommandHandler(ITheaterRepository theaterRepsitory , IUnitOfWork unitOfWork,ILogger<DeleteTheaterCommandHandler> logger) : IRequestHandler<DeleteTheaterCommand, ApiResult<bool>>
    {
        public async Task<ApiResult<bool>> Handle(DeleteTheaterCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin: DeleteTheaterCommandHandler");
            var theater = await theaterRepsitory.FindByIdAsync(request.Id);
            if (theater is null) return new ApiErrorResult<bool>("theater Id not found");
            await theaterRepsitory.DeleteAsync(request.Id);
            var result = await unitOfWork.SaveChangeAsync(cancellationToken);
            logger.LogInformation("end: DeleteTheaterCommandHandler");
            return result > 0 ? new ApiSuccessResult<bool>("Delete Theater successfully") : new ApiErrorResult<bool>("Error occurred while delete theater");
        }
    }
}
