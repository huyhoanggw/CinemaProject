using MediatR;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Seat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Seat.Commands.Update
{
    public class UpdateSeatCommand : IRequest<ApiResult< Domain.Enitities.Seat>>
    {
        public Guid Id { get; set; }
        public string Row { get; set; }
        public int Number { get; set; }
    }
}
