using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Domain.Enitities
{
    public class ShowtimeSeat : BaseEntity
    {
        public Guid ShowtimeId { get; set; }

        public Showtime Showtime { get; set; } = default!;

        public Guid SeatId { get; set; }

        public Seat Seat { get; set; } = default!;

        public decimal Price { get; set; }

        public ShowtimeSeatStatus Status { get; set; }

        public DateTime? HeldUntil { get; set; }

        public ICollection<BookingSeat> BookingSeats { get; set; } = [];
    }
}
