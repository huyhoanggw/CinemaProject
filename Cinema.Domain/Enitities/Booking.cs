using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Domain.Enitities
{
    public class Booking : BaseEntity
    {
        public string UserId { get; set; } = default!;

        public Guid ShowtimeId { get; set; }

        public Showtime Showtime { get; set; } = default!;

        public string BookingCode { get; set; } = default!;

        public decimal TotalPrice { get; set; }

        public BookingStatus Status { get; set; }

        public ICollection<BookingSeat> BookingSeats { get; set; } = [];
        public ICollection<BookingFood> BookingFoods { get; set; } = [];
        public DateTime ExpiredAt { get; set; }
        public Payment? Payment { get; set; }
    }
}
        