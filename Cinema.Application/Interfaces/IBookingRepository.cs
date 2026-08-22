using Cinema.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Interfaces
{
    public interface IBookingRepository : IBaseRepository<Booking>
    {
        public Task<Booking> GetBookingByIdAndUserId(Guid bookingId, string userId);
        public Task<IEnumerable<Booking>> GetBookingsById(List<Guid> Id);
        public Task<IEnumerable<Booking>> GetBookingsByUserId(string UserId);
        public Task<Booking> GetBookingByUserId(string UserId , string BookingId);
    }
}
