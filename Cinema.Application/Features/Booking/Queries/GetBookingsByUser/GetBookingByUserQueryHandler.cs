using Cinema.Application.Interfaces;
using Cinema.Contracts.Reponse;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Booking.Queries.GetBookingsByUser
{
    public class GetBookingByUserQueryHandler(IBookingRepository bookingRepository ,IHttpContextAccessor httpcontext 
        , ILogger<GetBookingByUserQueryHandler> logger) : IRequestHandler<GetBookingByUserQuery, ApiResult<List<Domain.Enitities.Booking>>>
    {
        public async  Task<ApiResult<List<Domain.Enitities.Booking>>> Handle(GetBookingByUserQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin: GetBookingByUserQueryHandler");
            var userId = httpcontext.HttpContext.User.FindFirst("sub")?.Value ?? httpcontext.HttpContext.User.FindFirst("uid")?.Value
            ?? httpcontext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null) return new ApiErrorResult<List<Domain.Enitities.Booking>>("User Id not found");
            var bookings = await bookingRepository.GetBookingsByUserId(userId);
            logger.LogInformation("end: GetBookingByUserQueryHandler");
            return new ApiSuccessResult<List<Domain.Enitities.Booking>>(bookings.ToList(), "get the bookings success");
        }
    }
}
