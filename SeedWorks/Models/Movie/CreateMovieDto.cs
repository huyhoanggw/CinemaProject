using Cinema.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedWorks.Models.Movie
{
    public record CreateMovieDto {
        public string Name { get; init; } = default!;
           public string? Description { get; init; }
        public List<Guid> MovieGenres { get; init; }
        public string? PosterUrl { get; init; }
        public string? TrailerUrl { get; init; }

    }
 }
