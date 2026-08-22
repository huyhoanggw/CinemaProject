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

namespace Cinema.Application.Features.Food.Commands.Delete
{
    public class DeleteFoodCommandHandler(IFoodRepository FoodRepsitory , IUnitOfWork unitOfWork,ILogger<DeleteFoodCommandHandler> logger) : IRequestHandler<DeleteFoodCommand, ApiResult<bool>>
    {
        public async Task<ApiResult<bool>> Handle(DeleteFoodCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin: DeleteFoodCommandHandler");
            var Food = await FoodRepsitory.FindByIdAsync(request.Id);
            if (Food is null) return new ApiErrorResult<bool>("Food Id not found");
            await FoodRepsitory.DeleteAsync(request.Id);
            var result = await unitOfWork.SaveChangeAsync(cancellationToken);
            logger.LogInformation("end: DeleteFoodCommandHandler");
            return result > 0 ? new ApiSuccessResult<bool>("Delete Food successfully") : new ApiErrorResult<bool>("Error occurred while delete Food");
        }
    }
}
