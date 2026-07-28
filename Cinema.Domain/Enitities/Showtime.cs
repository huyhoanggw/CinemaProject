using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Domain.Enitities
{
    public class Showtime : BaseEntity
    {
        public Guid MovieId { get; set; }

        public Movie Movie { get; set; } = default!;

        public Guid TheaterId { get; set; }

        public Theater Theater { get; set; } = default!;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public decimal BasePrice { get; set; }

        public ShowtimeStatus Status { get; set; }

        public ICollection<ShowtimeSeat> ShowtimeSeats { get; set; } = [];

        public ICollection<Booking> Bookings { get; set; } = [];
    }
}
