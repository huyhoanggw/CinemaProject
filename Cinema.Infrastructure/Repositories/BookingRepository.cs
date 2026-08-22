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
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        private readonly CinemaDbcontext _context;
        private readonly ILogger<BaseRepository<Booking>> _logger;

        public BookingRepository(CinemaDbcontext context, ILogger<BaseRepository<Booking>> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Booking> GetBookingByIdAndUserId(Guid bookingId, string userId)
        {
            return await _context.Set<Booking>().Where(x=> x.Id == bookingId && x.UserId == userId).FirstOrDefaultAsync() ?? null;
        }

        public async Task<Booking> GetBookingByUserId(string UserId , string BookingId)
        {
            return await _context.Set<Booking>().Where(x => x.UserId.Equals(UserId) && x.Id.Equals(BookingId)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsById(List<Guid> Id)
        {
           return await _context.Set<Booking>().Where(x => Id.Contains(x.Id)).ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUserId(string UserId)
        {
            return await _context.Set<Booking>().Where(x => x.UserId.Equals(UserId)).ToListAsync();
        }
    }
}
