using Cinema.Contracts.Reponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Food.Queries.GetFoodByName
{
    public class GetFoodByNameQuery : IRequest<ApiResult<Domain.Enitities.Food>>
    {
        public string name { get; set; }
    }
}
