using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Domain.Enitities
{
    public class Movie : BaseEntity
    {
        public int Name { get; set; }
        public string Description { get; set; } = " Mô tả trống";
        public ICollection<MovieGenre> MovieGenre { get; set; } = [];
        public string? PosterUrl { get; set; }

        public string? TrailerUrl { get; set; }

    }
}
