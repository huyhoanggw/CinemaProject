using Cinema.Application.Interfaces;
using Cinema.Domain.Enitities;
using Cinema.Infrastructure.Database;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Repositories
{
    public class MovieGenreRepository : BaseRepository<MovieGenre>, IMovieGenreRepository
    {
        public MovieGenreRepository(CinemaDbcontext _context, ILogger<BaseRepository<MovieGenre>> _logger) : base(_context, _logger)
        {
        }
    }
}
