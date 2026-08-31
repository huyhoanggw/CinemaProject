using Cinema.Api.Attribute;
using Cinema.Api.Constant;
using Cinema.Application.Features.Showtime.Commands.Create;
using Cinema.Application.Features.Showtime.Commands.Delete;
using Cinema.Application.Features.Showtime.Commands.Update;
using Cinema.Application.Features.Showtime.Queries.GetShowtimeById;
using Cinema.Application.Features.Showtime.Queries.Pagination;
using Cinema.Application.Interfaces;
using Cinema.Domain.Enitities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeedWorks.ApiReponse;

namespace Cinema.Api.Controllers
{
    [Authorize]    
    
    public class ShowtimeController(IMediator mediator , ILogger<ShowtimeController> logger) : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetShowtimes([FromQuery] int PageNumber , int Pagesize)
        {
            var result = await mediator.Send(new GetTheaterPagingQuery() { PageSize = Pagesize , PageNumber = PageNumber});
            return Ok(result);
                
        }
        [PermissionAttribute(Permission.ShowtimeCreate)]
        [HttpPost]
        public async Task<IActionResult> AddShowtime([FromBody]CreateShowtimeCommand request)
        {
            var result = await mediator.Send(request);
            return result.IsSuccess ? Ok(result) : NotFound(result);
                
        }

        [PermissionAttribute(Permission.ShowtimeUpdate)]
        [HttpPut]
        public async Task<IActionResult> UpdateShowtime([FromBody] UpdateShowtimeCommand request)
        {
            var result = await mediator.Send(request);
            return result.IsSuccess ? Ok(result) : NotFound(result);

        }
        [PermissionAttribute(Permission.ShowtimeDelete)]
        [HttpDelete]
        public async Task<IActionResult> DeleteShowtime([FromQuery] Guid Id)
        {
            var result = await mediator.Send(new DeleteShowtimeCommand() { ShowtimeId = Id } );
            return result.IsSuccess ? Ok(result) : NotFound(result);

        }
    }
}
