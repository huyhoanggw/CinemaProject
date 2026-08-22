using AutoMapper;
using Cinema.Application.Features.Showtime.Queries.GetShowtimeById;
using Cinema.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Seat;
using SeedWorks.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Seat.Commands.Create
{
    public class CreateFoodCommandHandler(ISeatRepository repository , IMapper mapper , IUnitOfWork unitOfWork,ILogger<CreateFoodCommandHandler> logger) : IRequestHandler<CreateFoodCommand, ApiResult<Domain.Enitities.Seat>>
    {
        public async Task<ApiResult<Domain.Enitities.Seat>> Handle(CreateFoodCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin:GetShowtimeByIdQueryHandler");
            var Seat = await repository.GetByAsync(x=> x.Number == request.Number && x.Row == request.Row);
            if (Seat is not null) return new ApiErrorResult<Domain.Enitities.Seat> ("Seat is duplicate");
            await repository.CreateAsync(new Domain.Enitities.Seat()
            {
                Id = Guid.NewGuid(),
                CreateAt = DateTime.Now,
                Row = request.Row,
                Number = request.Number,

            });
            var result = await unitOfWork.SaveChangeAsync(cancellationToken);
            var SeatFromMapper = mapper.Map< Domain.Enitities.Seat> (Seat);
            logger.LogInformation("end:GetShowtimeByIdQueryHandler");
            return result >= 0 ? new ApiSuccessResult<Domain.Enitities.Seat> (SeatFromMapper, "Create Seat successfully") : new ApiErrorResult<Domain.Enitities.Seat> ("Error occurred while create Seat");
        }
    }
}
