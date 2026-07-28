using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Domain.Enitities
{
    public class MovieGenre
    {
        public Guid MovieId { get; set; }

        public Movie Movie { get; set; } = default!;

        public Guid GenreId { get; set; }

        public Genre Genre { get; set; } = default!;
    }
}
