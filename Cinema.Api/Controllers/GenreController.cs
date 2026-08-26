using Cinema.Api.Attribute;
using Cinema.Api.Constant;
using Cinema.Application.Features.Booking.Queries.Pagination;
using Cinema.Application.Features.Genre.Commands.Create;
using Cinema.Application.Features.Genre.Commands.Update;
using Cinema.Application.Features.Genre.Queries.GetGenreById;
using Cinema.Application.Features.Genre.Queries.Pagination;
using Cinema.Application.Interfaces;
using Cinema.Domain.Enitities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeedWorks.ApiReponse;

namespace Cinema.Api.Controllers
{
    [Authorize]    
    
    public class GenreController(IMediator mediator , ILogger<GenreController> logger) : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var result = await mediator.Send(new GetGenrePagingQuery() {PageNumber = 1 , PageSize = 10 });
            return Ok(result);
                
        }
        [PermissionAttribute(Permission.GenreCreate)]
        [HttpPost]
        public async Task<IActionResult> AddGenre([FromBody]CreateGenreCommand request)
        {
            var result = await mediator.Send(request);
            return result.IsSuccess ? Ok(result) : NotFound(result);
                
        }
        [PermissionAttribute(Permission.GenreUpdate)]
        [HttpPut]
        public async Task<IActionResult> UpdateGenre([FromBody]UpdateGenreCommand request)
        {
            var result = await mediator.Send(request);
            return result.IsSuccess ? Ok(result) : NotFound(result);
                
        }
        [PermissionAttribute(Permission.GenreDelete)]
        [HttpDelete]
        public async Task<IActionResult> DeleteGenre([FromBody]UpdateGenreCommand request)
        {
            var result = await mediator.Send(request);
            return result.IsSuccess ? Ok(result) : NotFound(result);
                
        }
    }
}
