
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

namespace Cinema.Application.Features.Food.Queries.GetFoodById
{
    public class GetFoodByIdQueryHandler(IFoodRepository FoodRepository , ILogger<GetFoodByIdQueryHandler> logger): IRequestHandler<GetFoodByIdQuery, ApiResult<Domain.Enitities.Food>>
    {
        public async Task<ApiResult<Domain.Enitities.Food>> Handle(GetFoodByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin: GetFoodByIdQueryHandler");
            var Food = await FoodRepository.FindByIdAsync(request.Id);
            if (Food == null) return new ApiErrorResult<Domain.Enitities.Food>("Food Id not found");
            logger.LogInformation("end: GetFoodByIdQueryHandler");
            return new ApiSuccessResult<Domain.Enitities.Food>(Food, "Get Food Successfully");
        }
    }
}
