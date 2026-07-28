using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Domain.Enitities
{
    public class BookingFood
    {
        public Guid BookingId { get; set; }
        public Booking Booking { get; set; }
        public Guid FoodId { get; set; }
        public Food Food { get; set; }
        public int Quanlity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
