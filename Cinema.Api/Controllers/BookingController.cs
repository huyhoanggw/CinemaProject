using Cinema.Api.Attribute;
using Cinema.Api.Constant;
using Cinema.Application.Features.Booking.Commands.Cancel;
using Cinema.Application.Features.Booking.Commands.Create;
using Cinema.Application.Features.Booking.Queries.GetBookingById;
using Cinema.Application.Features.Booking.Queries.Pagination;
using Cinema.Application.Interfaces;
using Cinema.Domain.Enitities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SeedWorks.ApiReponse;

namespace Cinema.Api.Controllers
{
    [Authorize]    
    
    public class BookingController(IMediator mediator , ILogger<BookingController> logger) : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetBookings([FromQuery] int pageSize, int pageNumber)
        {
            var result = await mediator.Send(new GetBookingPagingQuery() { PageNumber = pageNumber , PageSize = pageSize});
            return Ok(result);
                
        }
        [PermissionAttribute(Permission.BookingCreate)]
        [HttpPost]
        public async Task<IActionResult> AddBooking([FromBody]CreateBookingCommand request)
        {
            var result = await mediator.Send(request);
            return result.IsSuccess ? Ok(result) : NotFound(result);
                
        }

        [PermissionAttribute(Permission.BookingDelete)]
        [HttpDelete]
        public async Task<IActionResult> DeleteBooking([FromQuery] Guid Id)
        {
            var result = await mediator.Send(new CancelBookingCommand() { bookingId = Id } );
            return result.IsSuccess ? Ok(result) : NotFound(result);

        }
    }
}
