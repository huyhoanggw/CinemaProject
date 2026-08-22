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
    public class ShowtimeSeatRepository : BaseRepository<ShowtimeSeat>, IShowtimeSeatRepository
    {
        private readonly CinemaDbcontext context;

        public ShowtimeSeatRepository(CinemaDbcontext _context, ILogger<BaseRepository<ShowtimeSeat>> _logger) : base(_context, _logger)
        {
            context = _context; 
        }

        public async Task<List<ShowtimeSeat>> GetByIds(IEnumerable<Guid> ShowtimeSeatIds)
        {
            return await context.Set<ShowtimeSeat>().Where(x =>  ShowtimeSeatIds.Contains(x.Id)).ToListAsync();
        }

        public async Task<List<ShowtimeSeat>> GetByShowtimeAndSeatIdsAsync(Guid showtimeId, IEnumerable<Guid> seatId)
        {
            return await  context.Set<ShowtimeSeat>().Where(x => x.ShowtimeId == showtimeId && seatId.Contains(x.SeatId)).ToListAsync();
        }
    }
}
