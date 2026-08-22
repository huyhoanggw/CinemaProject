using MediatR;
using SeedWorks.ApiReponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Food.Queries.GetFoodById
{
    public class GetFoodByIdQuery : IRequest<ApiResult<Domain.Enitities.Food>>
    {
        public Guid Id { get; set; }
    }
}

