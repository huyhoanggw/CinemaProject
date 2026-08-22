using MediatR;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Seat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Seat.Commands.Create
{
    public class CreateFoodCommand : IRequest<ApiResult<Domain.Enitities.Seat>>
    {
        public string Row { get; set; }
        public int Number {  get; set; }
    }
}
