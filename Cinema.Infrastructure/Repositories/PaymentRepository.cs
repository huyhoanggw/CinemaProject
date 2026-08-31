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
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        private readonly CinemaDbcontext _context;
        private readonly ILogger<BaseRepository<Payment>> _logger;

        public PaymentRepository(CinemaDbcontext context, ILogger<BaseRepository<Payment>> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Payment> GetByBookingCode(string bookingCode)
        {
            return await _context.Set<Payment>().Where(x => x.Booking.BookingCode == bookingCode).FirstOrDefaultAsync();  
        }
    }
}
