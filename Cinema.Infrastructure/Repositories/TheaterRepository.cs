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
    public class TheaterRepository : BaseRepository<Theater>, ITheaterRepository
    {
        private readonly CinemaDbcontext _context;
        private readonly ILogger<BaseRepository<Theater>> _logger;

        public TheaterRepository(CinemaDbcontext context, ILogger<BaseRepository<Theater>> logger) : base(context, logger)
        {
            _context=context;
            _logger = logger;
        }

        public async Task<Theater?> FindByIdWithSeatsAsync(Guid id)
        {
            return await _context.Set<Theater>().Where(x => x.Id == id).Include(x => x.Seats).FirstOrDefaultAsync();
        }

        public async Task<Theater> GetTheaterByName(string name)
        {
            return await _context.Set<Theater>().Where(x => x.Name.Equals(name)).FirstOrDefaultAsync();
        }
    }
}
