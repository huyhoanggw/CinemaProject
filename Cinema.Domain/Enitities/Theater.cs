using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Domain.Enitities
{
    public class Theater : BaseEntity
    {
    
        public string Name { get; set; } = default!;

        public ICollection<Seat> Seats { get; set; } = [];

        public ICollection<Showtime> Showtimes { get; set; } = [];
    }
}
