using AutoMapper;
using Cinema.Application.Features.Showtime.Queries.GetShowtimeById;
using Cinema.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Food;
using SeedWorks.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Food.Commands.Create
{
    public class CreateFoodCommandHandler(IFoodRepository repository , IMapper mapper , IUnitOfWork unitOfWork,ILogger<GetShowtimeByIdQueryHandler> logger) : IRequestHandler<CreateFoodCommand, ApiResult<CreateFoodModel>>
    {
        public async Task<ApiResult<CreateFoodModel>> Handle(CreateFoodCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin:GetShowtimeByIdQueryHandler");
            var Food = await repository.GetByAsync(x => x.Name.Equals(request.Name));
            if (Food is not null) return new ApiErrorResult<CreateFoodModel>("Food is duplicate");
            await repository.CreateAsync(new Domain.Enitities.Food()
            {
                Id = Guid.NewGuid(),
                CreateAt = DateTime.Now,
                Name = request.Name,
                Quanlity = request.Quanlity,
                Price = request.Price
            });
            var result = await unitOfWork.SaveChangeAsync(cancellationToken);
            var FoodFromMapper = mapper.Map<CreateFoodModel>(Food);
            logger.LogInformation("end:GetShowtimeByIdQueryHandler");
            return result >= 0 ? new ApiSuccessResult<CreateFoodModel>(FoodFromMapper, "Create Food successfully") : new ApiErrorResult<CreateFoodModel>("Error occurred while create Food");
        }
    }
}
