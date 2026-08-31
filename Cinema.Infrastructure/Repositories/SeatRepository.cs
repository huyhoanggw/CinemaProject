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
    public class SeatRepository : BaseRepository<Seat>, ISeatRepository
    {
        private readonly CinemaDbcontext _context;
        private readonly ILogger<BaseRepository<Seat>> _logger;

        public SeatRepository(CinemaDbcontext context, ILogger<BaseRepository<Seat>> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Seat>> GetSeatsByIds(List<Guid> Ids)
        {
            return await  _context.Set<Seat>().Where(x => Ids.Equals(x.Id)).ToListAsync();
        }

          }
}
