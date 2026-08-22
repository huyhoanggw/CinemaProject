using Cinema.Domain.Enitities;
using MediatR;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Showtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Showtime.Commands.Update
{
    public class UpdateShowtimeCommand : IRequest<ApiResult<UpdateShowtimeModel>>
    {
        public Guid ShowtimeId { get; set; }
       
        public Cinema.Domain.Enitities.Movie Movie { get; set; } = default!;

       
        public Cinema.Domain.Enitities.Theater Theater { get; set; } = default!;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public decimal BasePrice { get; set; }

        public ShowtimeStatus Status { get; set; }
    }
}
