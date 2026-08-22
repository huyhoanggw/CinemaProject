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
    public class BookingSeatRepository : BaseRepository<BookingSeat>, IBookingSeatRepository
    {
        private readonly CinemaDbcontext _context;
        private readonly ILogger<BaseRepository<BookingSeat>> _logger;

        public BookingSeatRepository(CinemaDbcontext context, ILogger<BaseRepository<BookingSeat>> logger) : base(context, logger)
        {   
            _context = context;
            _logger = logger;
        }

        public async Task AddRange(IEnumerable<BookingSeat> bookingSeats)
        {
            await _context.AddRangeAsync(bookingSeats);
        }

        public Task<IEnumerable<BookingSeat>> GetBookingSeatByIds(List<Guid> Id)
        {
            throw new NotImplementedException();
        }
    }
}
