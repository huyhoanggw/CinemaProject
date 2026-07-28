using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Domain.Enitities
{
    public class BookingSeat
    {
        public Guid BookingId { get; set; }

        public Booking Booking { get; set; } = default!;

        public Guid ShowtimeSeatId { get; set; }

        public ShowtimeSeat ShowtimeSeat { get; set; } = default!;

        public decimal Price { get; set; }
    }
}
