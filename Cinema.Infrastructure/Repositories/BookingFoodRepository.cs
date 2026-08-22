using Cinema.Application.Interfaces;
using Cinema.Domain.Enitities;
using Cinema.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Repositories
{
    public class BookingFoodRepository :BaseRepository<BookingFood>, IBookingFoodRepository
    {
        private readonly CinemaDbcontext _context;
        private readonly ILogger<BaseRepository<BookingFood>> _logger;

        public BookingFoodRepository(CinemaDbcontext context, ILogger<BaseRepository<BookingFood>> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddRange(IEnumerable<BookingFood> bookingFoods)
        {
                await _context.AddRangeAsync(bookingFoods);
               
        }

        public async Task<IEnumerable<BookingFood>> getByIdsAndBookingId(Guid bookingId, List<Guid> BookingFoodId)
        {
            return await _context.Set<BookingFood>().Where(x => x.BookingId == bookingId && BookingFoodId.Contains(x.FoodId)).ToListAsync();
        }
    }
}
