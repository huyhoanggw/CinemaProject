using MediatR;
using SeedWorks.ApiReponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Showtime.Commands.Delete
{
    public class DeleteShowtimeCommand : IRequest<ApiResult<bool>>
    {
        public Guid ShowtimeId { get; set; }
    }
}
