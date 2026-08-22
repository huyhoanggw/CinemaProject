using AutoMapper;
using Cinema.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Food;
using SeedWorks.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Food.Commands.Update
{
    public class UpdateFoodCommandHandler(IFoodRepository FoodRepository , IMapper mapper , ILogger<UpdateFoodCommandHandler> logger , IUnitOfWork unitOfWork )
        : IRequestHandler<UpdateFoodCommand, ApiResult<UpdateFoodModel>>
    {
        public async Task<ApiResult<UpdateFoodModel>> Handle(UpdateFoodCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin:UpdateFoodCommandHandler");
            var Food = await FoodRepository.FindByIdAsync(request.Id );
            if (Food is null) return new ApiErrorResult<UpdateFoodModel>("Food Id Not found");
            Food.Name = request.Name;
            Food.Price = request.Price;
            Food.Quanlity = request.Quanlity;
            var result = await unitOfWork.SaveChangeAsync(cancellationToken);
            logger.LogInformation("end:UpdateFoodCommandHandler");
            var UpdateTomapper = mapper.Map<UpdateFoodModel>(Food);
            return result >= 0 ? new ApiSuccessResult<UpdateFoodModel>(UpdateTomapper, "update Food successfully") : new ApiErrorResult<UpdateFoodModel>("Error occurred while update Food");
        }
    }
}
