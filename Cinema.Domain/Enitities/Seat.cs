using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Domain.Enitities
{
    public class Seat : BaseEntity
    {
        public Guid TheaterId { get; set; }

        public Theater Theater { get; set; } = default!;

        public string Row { get; set; } = default!;

        public int Number { get; set; }

        
        public decimal PriceMultiplier { get; set; }

        public ICollection<ShowtimeSeat> ShowtimeSeats { get; set; } = [];
    }
}
