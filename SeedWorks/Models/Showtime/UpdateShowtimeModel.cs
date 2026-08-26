using Cinema.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedWorks.Models.Showtime
{
    public record UpdateShowtimeModel
    {
        public Cinema.Domain.Enitities.Movie Movie { get; set; }


        public Cinema.Domain.Enitities.Theater Theater { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public decimal BasePrice { get; set; }

        public ShowtimeStatus Status { get; set; }

           }

}
