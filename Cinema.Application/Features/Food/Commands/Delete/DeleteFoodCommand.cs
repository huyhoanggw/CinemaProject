using MediatR;
using SeedWorks.ApiReponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Food.Commands.Delete
{
    public class DeleteFoodCommand : IRequest<ApiResult<bool>>
    {
        public Guid Id { get; set; }
    }
}
