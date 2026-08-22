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

namespace Cinema.Application.Features.Booking.Commands.Cancel
{
    public class CancelBookingCommandHandler(IBookingRepository bookingRepository , IHttpContextAccessor httpcontext , IUnitOfWork unitofWork 
        , ILogger<CancelBookingCommandHandler> logger) : IRequestHandler<CancelBookingCommand, ApiResult<bool>>
    {
        public async Task<ApiResult<bool>> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin:CancelBookingCommandHandler ");
            var userId = httpcontext.HttpContext.User?.FindFirst("uid")?.Value ?? httpcontext.HttpContext.User?.FindFirst("sub")?.Value
             ?? httpcontext.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException();
            }
            var booking = await bookingRepository.FindByIdAsync(request.bookingId);
                if (booking is null) return new ApiErrorResult<bool>("Booking Id not found");
                foreach(var bookingSeat in booking.BookingSeats)
            {
                var seat = bookingSeat.ShowtimeSeat;
                if(seat.Status == Domain.Enitities.ShowtimeSeatStatus.Hold && seat.ReservedBy == userId)
                {
                    seat.Status =Domain.Enitities.ShowtimeSeatStatus.Available;
                    seat.ReservedBy = null;
                    seat.ReservedUntil = null;
                    seat.ReservedAt = null;
                }
            }
            booking.Status = Domain.Enitities.BookingStatus.Cancelled;
               var result= await unitofWork.SaveChangeAsync(cancellationToken);
            logger.LogInformation("end:CancelBookingCommandHandler");
            return result > 0 ? new ApiSuccessResult<bool>("Canceled booking successfully") : new ApiErrorResult<bool>("Error occurred while cancel booking");
        }
    }
}
