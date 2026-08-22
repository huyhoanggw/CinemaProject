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

namespace Cinema.Application.Features.Food.Queries.GetFoodByName
{
    public class GetFoodByNameQueryHandler(IFoodRepository foodRepository , ILogger<GetFoodByNameQuery> logger) : IRequestHandler<GetFoodByNameQuery, ApiResult<Domain.Enitities.Food>>
    {
        public async Task<ApiResult<Domain.Enitities.Food>> Handle(GetFoodByNameQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin : GetFoodByNameQueryHandler");
            var food = await foodRepository.GetByAsync(x => x.Name.Equals(request.name));
            if (food is null) return new ApiErrorResult<Domain.Enitities.Food>("Food Name not found");
            logger.LogInformation("end: GetFoodByNameQueryHandler");
            return new ApiSuccessResult<Domain.Enitities.Food>("get food by name success");
        }
    }
}
