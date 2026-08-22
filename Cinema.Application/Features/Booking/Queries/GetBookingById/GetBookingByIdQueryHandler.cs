using AutoMapper;
using Cinema.Application.Interfaces;
using Cinema.Domain.Enitities;
using MediatR;
using Microsoft.Extensions.Logging;
using SeedWorks.ApiReponse;
using SeedWorks.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Booking.Queries.GetBookingById
{
    public class GetBookingByIdQueryHandler(IBookingRepository bookingRepository , IMapper mapper , ILogger<GetBookingByIdQueryHandler> logger) : IRequestHandler<GetBookingByIdQuery, ApiResult<Domain.Enitities.Booking>>
    {
        public async  Task<ApiResult<Domain.Enitities.Booking>> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Start: GetBookingByIdQueryHandler.");
                var booking = await bookingRepository.FindByIdAsync(request.Id);
            if (booking is null) return new ApiErrorResult<Domain.Enitities.Booking>("Booking Information not found ");
            logger.LogInformation("End: GetBookingByIdQueryHandler.");
            return new ApiSuccessResult<Domain.Enitities.Booking>("");
        }
    }
}
