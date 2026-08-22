using Cinema.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedWorks.Models.Movie
{
    public record UpdateMovieModel(
             string Name,
            string? Description,
            List<MovieGenre> MovieGenres,
            string? PosterUrl,
            string? TrailerUrl
        );
   }
