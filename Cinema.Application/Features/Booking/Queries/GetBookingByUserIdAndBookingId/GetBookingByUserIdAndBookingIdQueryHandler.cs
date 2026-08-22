using Cinema.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SeedWorks.ApiReponse;
using SeedWorks.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Booking.Queries.GetBookingByUserIdAndBookingId
{
    public class GetBookingByUserIdAndBookingIdQueryHandler(IBookingRepository bookingRepository , ILogger<GetBookingByUserIdAndBookingIdQueryHandler> logger 
        , IHttpContextAccessor httpcontext) : IRequestHandler<GetBookingByUserIdAndBookingIdQuery, ApiResult<Cinema.Domain.Enitities.Booking>>
    {
        public async Task<ApiResult<Domain.Enitities.Booking>> Handle(GetBookingByUserIdAndBookingIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin : GetBookingByUserIdAndBookingIdQueryHandler");
            var userId = httpcontext.HttpContext.User.FindFirst("sub")?.Value ?? httpcontext.HttpContext.User.FindFirst("uid")?.Value
                ?? httpcontext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId is null) return new ApiErrorResult<Domain.Enitities.Booking>("User Id not found ");
            var booking = await bookingRepository.GetBookingByUserId(userId, request.BookingId);
            if(booking is null) return new ApiErrorResult<Domain.Enitities.Booking>("Booking not found ");
            logger.LogInformation("begin : GetBookingByUserIdAndBookingIdQueryHandler");
            return new ApiSuccessResult<Domain.Enitities.Booking>(booking, "Get Booking success");
        }
    }
}
