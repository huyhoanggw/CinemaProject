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
    public class BookingExpirationRepository(CinemaDbcontext dbcontext , ILogger<BookingExpirationRepository> logger) : IBookingExpirationService
    {
        public async Task ExpireAsync(CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var ExpiredBookings = await dbcontext.Set<Booking>()
                .Include(x => x.BookingSeats).ThenInclude(x => x.ShowtimeSeat).Where(x => x.Status == BookingStatus.Pending && x.ExpiredAt <= now).ToListAsync();
            foreach(var booking in ExpiredBookings)
            {
                booking.Status = BookingStatus.Expired;
                foreach(var bookingSeat in booking.BookingSeats)
                {
                    var seat = bookingSeat.ShowtimeSeat;
                    if(seat.Status == ShowtimeSeatStatus.Hold && seat.ReservedBy == booking.UserId)
                    {
                        seat.Status = ShowtimeSeatStatus.Available;
                        seat.ReservedAt = null;
                        seat.ReservedBy = null;
                        seat.ReservedUntil = null;
                    }
                }
            }
          await dbcontext.SaveChangesAsync();
        }
    }
}
