using AutoMapper;
using Cinema.Application.Features.Seat.Queries.GetSeatById;
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
    public class CreateSeatCommandHandler(ISeatRepository repository , IMapper mapper , IUnitOfWork unitOfWork,ILogger<CreateSeatCommandHandler> logger) : IRequestHandler<CreateSeatCommand, ApiResult<Domain.Enitities.Seat>>
    {
        public async Task<ApiResult<Domain.Enitities.Seat>> Handle(CreateSeatCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin:GetSeatByIdQueryHandler");
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
            logger.LogInformation("end:GetSeatByIdQueryHandler");
            return result >= 0 ? new ApiSuccessResult<Domain.Enitities.Seat> (SeatFromMapper, "Create Seat successfully") : new ApiErrorResult<Domain.Enitities.Seat> ("Error occurred while create Seat");
        }
    }
}
