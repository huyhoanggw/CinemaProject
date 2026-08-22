using MediatR;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Showtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Showtime.Commands.Create
{
    public class CreateShowtimeCommand : IRequest<ApiResult<CreateShowtimeModel>>
    {
        public Guid MovieId { get; set; }
        public Guid TheaterId { get; set; }
         public  DateTime StartTime { get; set; }

         public DateTime EndTime { get; set; }

         public decimal BasePrice { get; set; }
        
    }
}
