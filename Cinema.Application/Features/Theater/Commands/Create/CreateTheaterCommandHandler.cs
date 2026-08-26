using AutoMapper;
using Cinema.Application.Features.Theater.Queries.GetTheaterById;
using Cinema.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Theater;
using SeedWorks.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Theater.Commands.Create
{
    public class CreateTheaterCommandHandler(ITheaterRepository repository , IMapper mapper , IUnitOfWork unitOfWork,ILogger<CreateTheaterCommandHandler> logger) : IRequestHandler<CreateTheaterCommand, ApiResult<CreateTheaterModel>>
    {
        public async Task<ApiResult<CreateTheaterModel>> Handle(CreateTheaterCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin:GetTheaterByIdQueryHandler");
            var theater = await repository.GetTheaterByName(request.Name);
            if (theater is not null) return new ApiErrorResult<CreateTheaterModel>("Theater is duplicate");
            await repository.CreateAsync(new Domain.Enitities.Theater()
            {
                Id = Guid.NewGuid(),
                CreateAt = DateTime.Now,
                Name = request.Name
            });
            var result = await unitOfWork.SaveChangeAsync(cancellationToken);
            var TheaterFromMapper = mapper.Map<CreateTheaterModel>(theater);
            logger.LogInformation("end:GetTheaterByIdQueryHandler");
            return result >= 0 ? new ApiSuccessResult<CreateTheaterModel>(TheaterFromMapper, "Create Theater successfully") : new ApiErrorResult<CreateTheaterModel>("Error occurred while create theater");
        }
    }
}
