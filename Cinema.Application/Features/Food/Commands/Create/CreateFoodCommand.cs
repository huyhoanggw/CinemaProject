using MediatR;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Food.Commands.Create
{
    public class CreateFoodCommand : IRequest<ApiResult<CreateFoodModel>>
    {
        public string Name { get; set; }
        public int Quanlity { get; set; }
        public decimal Price { get; set; }
    }
}
