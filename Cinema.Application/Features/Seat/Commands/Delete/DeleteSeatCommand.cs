using Cinema.Contracts.Reponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Seat.Commands.Delete
{
    public class DeleteSeatCommand : IRequest<ApiResult<bool>>
    {
        public Guid Id { get; set; }
    }
}
