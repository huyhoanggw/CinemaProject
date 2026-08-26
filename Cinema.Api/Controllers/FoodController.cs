using Cinema.Api.Attribute;
using Cinema.Api.Constant;
using Cinema.Application.Features.Food.Commands.Create;
using Cinema.Application.Features.Food.Commands.Delete;
using Cinema.Application.Features.Food.Commands.Update;
using Cinema.Application.Features.Food.Queries.GetFoodById;
using Cinema.Application.Features.Food.Queries.Pagination;
using Cinema.Application.Interfaces;
using Cinema.Domain.Enitities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeedWorks.ApiReponse;

namespace Cinema.Api.Controllers
{
    [Authorize]    
    
    public class FoodController(IMediator mediator , ILogger<FoodController> logger) : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetFoods()
        {
            var result = await mediator.Send(new GetFoodPagingQuery());
            return Ok(result);
                
        }
        [PermissionAttribute(Permission.FoodCreate)]
        [HttpPost]
        public async Task<IActionResult> AddFood([FromBody]CreateFoodCommand request)
        {
            var result = await mediator.Send(request);
            return result.IsSuccess ? Ok(result) : NotFound(result);
                
        }

        [PermissionAttribute(Permission.FoodUpdate)]
        [HttpPut]
        public async Task<IActionResult> UpdateFood([FromBody] UpdateFoodCommand request)
        {
            var result = await mediator.Send(request);
            return result.IsSuccess ? Ok(result) : NotFound(result);

        }
        [PermissionAttribute(Permission.FoodDelete)]
        [HttpDelete]
        public async Task<IActionResult> DeleteFood([FromQuery] Guid Id)
        {
            var result = await mediator.Send(new DeleteFoodCommand() { Id = Id } );
            return result.IsSuccess ? Ok(result) : NotFound(result);

        }
    }
}
