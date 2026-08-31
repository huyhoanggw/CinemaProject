using Cinema.Api.Attribute;
using Cinema.Api.Constant;
using Cinema.Application.Features.Showtime.Queries.Pagination;
using Cinema.Application.Features.Theater.Commands.Create;
using Cinema.Application.Features.Theater.Commands.Delete;
using Cinema.Application.Features.Theater.Commands.Update;
using Cinema.Application.Features.Theater.Queries.GetTheaterById;
using Cinema.Application.Features.Theater.Queries.Pagination;
using Cinema.Application.Interfaces;
using Cinema.Domain.Enitities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeedWorks.ApiReponse;

namespace Cinema.Api.Controllers
{
    [Authorize]

    public class TheaterController(IMediator mediator, ILogger<TheaterController> logger) : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetTheaters([FromQuery] int pageSize , int pageNumber)
        {
            var result = await mediator.Send(new GetTheaterPagingQuery() { PageNumber = pageNumber , PageSize = pageSize});
            return Ok(result);
                
        }
        [PermissionAttribute(Permission.TheaterCreate)]
        [HttpPost]
        public async Task<IActionResult> AddTheater([FromBody]Theater request)
        {
            var result = await mediator.Send(new CreateTheaterCommand() { Name = request.Name });
            return result.IsSuccess ? Ok(result) : NotFound(result);
                
        }

        [PermissionAttribute(Permission.TheaterUpdate)]
        [HttpPut]
        public async Task<IActionResult> UpdateTheater([FromBody] Theater request)
        {
            var result = await mediator.Send(new UpdateTheaterCommand() { Name = request.Name});
            return result.IsSuccess ? Ok(result) : NotFound(result);

        }
        [PermissionAttribute(Permission.TheaterDelete)]
        [HttpDelete]
        public async Task<IActionResult> DeleteTheater([FromQuery] Guid Id)
        {
            var result = await mediator.Send(new DeleteTheaterCommand() { Id = Id } );
            return result.IsSuccess ? Ok(result) : NotFound(result);

        }
    }
}
