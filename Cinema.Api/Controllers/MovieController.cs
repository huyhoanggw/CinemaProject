using Cinema.Api.Attribute;
using Cinema.Api.Constant;
using Cinema.Application.Features.Movie.Commands.Create;
using Cinema.Application.Features.Movie.Commands.Delete;
using Cinema.Application.Features.Movie.Commands.Update;
using Cinema.Application.Features.Movie.Queries.GetAll;
using Cinema.Application.Features.Movie.Queries.GetMovieById;
using Cinema.Application.Features.Movie.Queries.Pagination;
using Cinema.Application.Interfaces;
using Cinema.Domain.Enitities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeedWorks.ApiReponse;

namespace Cinema.Api.Controllers
{
    [Authorize]    
    
    public class MovieController(IMediator mediator , ILogger<MovieController> logger) : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetMovies([FromQuery] int pageNumber , int PageSize)
        {
            var result = await mediator.Send(new GetMoviePagingQuery() {PageNumber = pageNumber , PageSize = PageSize});
            return Ok(result);
                
        }
        [PermissionAttribute(Permission.MovieCreate)]
        [HttpPost]
        public async Task<IActionResult> AddMovie([FromBody]CreateMovieCommand request)
        {
            var result = await mediator.Send(request);
            return result.IsSuccess ? Ok(result) : NotFound(result);
                
        }

        [PermissionAttribute(Permission.MovieUpdate)]
        [HttpPut]
        public async Task<IActionResult> UpdateMovie([FromBody] UpdateMovieCommand request)
        {
            var result = await mediator.Send(request);
            return result.IsSuccess ? Ok(result) : NotFound(result);

        }
        [PermissionAttribute(Permission.MovieDelete)]
        [HttpDelete]
        public async Task<IActionResult> DeleteShowtime([FromQuery] Guid Id)
        {
            var result = await mediator.Send(new DeleteMovieCommand() { Id = Id } );
            return result.IsSuccess ? Ok(result) : NotFound(result);

        }
    }
}
