using MediatR;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Food.Commands.Update
{
    public class UpdateFoodCommand : IRequest<ApiResult<UpdateFoodModel>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quanlity{ get; set; }
        public decimal Price{ get; set; }
    }
}
