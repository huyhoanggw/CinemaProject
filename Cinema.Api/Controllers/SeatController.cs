using Cinema.Api.Attribute;
using Cinema.Api.Constant;
using Cinema.Application.Features.Seat.Commands.Create;
using Cinema.Application.Features.Seat.Commands.Delete;
using Cinema.Application.Features.Seat.Commands.Update;
using Cinema.Application.Features.Seat.Queries.GetSeatById;
using Cinema.Application.Features.Seat.Queries.Pagination;
using Cinema.Application.Interfaces;
using Cinema.Domain.Enitities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeedWorks.ApiReponse;

namespace Cinema.Api.Controllers
{
    [Authorize]    
    
    public class SeatController(IMediator mediator , ILogger<SeatController> logger) : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetSeats()
        {
            var result = await mediator.Send(new GetSeatPagingQuery());
            return Ok(result);
                
        }
        [PermissionAttribute(Permission.SeatCreate)]
        [HttpPost]
        public async Task<IActionResult> AddSeat([FromBody]CreateSeatCommand request)
        {
            var result = await mediator.Send(request);
            return result.IsSuccess ? Ok(result) : NotFound(result);
                
        }

        [PermissionAttribute(Permission.SeatUpdate)]
        [HttpPut]
        public async Task<IActionResult> UpdateSeat([FromBody] UpdateSeatCommand request)
        {
            var result = await mediator.Send(request);
            return result.IsSuccess ? Ok(result) : NotFound(result);

        }
        [PermissionAttribute(Permission.SeatDelete)]
        [HttpDelete]
        public async Task<IActionResult> DeleteSeat([FromQuery] Guid Id)
        {
            var result = await mediator.Send(new DeleteSeatCommand() { Id = Id } );
            return result.IsSuccess ? Ok(result) : NotFound(result);

        }
    }
}
