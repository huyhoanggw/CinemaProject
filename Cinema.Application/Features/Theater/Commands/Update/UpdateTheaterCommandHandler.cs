using AutoMapper;
using Cinema.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Theater;
using SeedWorks.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Theater.Commands.Update
{
    public class UpdateFoodCommandHandler(ITheaterRepository theaterRepository , IMapper mapper , ILogger<UpdateFoodCommandHandler> logger , IUnitOfWork unitOfWork )
        : IRequestHandler<UpdateFoodCommand, ApiResult<UpdateTheaterModel>>
    {
        public async Task<ApiResult<UpdateTheaterModel>> Handle(UpdateFoodCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin:UpdateTheaterCommandHandler");
            var theater = await theaterRepository.FindByIdAsync(request.Id );
            if (theater is null) return new ApiErrorResult<UpdateTheaterModel>("Theater Id Not found");
            theater.Name = request.Name;
            var result = await unitOfWork.SaveChangeAsync(cancellationToken);
            logger.LogInformation("end:UpdateTheaterCommandHandler");
            var UpdateTomapper = mapper.Map<UpdateTheaterModel>(theater);
            return result >= 0 ? new ApiSuccessResult<UpdateTheaterModel>(UpdateTomapper, "update theater successfully") : new ApiErrorResult<UpdateTheaterModel>("Error occurred while update theater");
        }
    }
}
