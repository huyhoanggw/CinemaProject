using Cinema.Application.Interfaces;
using Cinema.Domain.Enitities;
using Cinema.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Repositories
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        private readonly CinemaDbcontext context;
        private readonly ILogger<BaseRepository<Genre>> logger;

        public GenreRepository(CinemaDbcontext _context, ILogger<BaseRepository<Genre>> _logger) : base(_context, _logger)
        {
            context = _context;
            logger= _logger;
        }

        public async  Task<List<Genre>> GetGenresByIds(List<Guid> Ids)
        {
            return await context.Set<Genre>().Where(x => Ids.Equals(Ids)).ToListAsync();
        }
    }
}
