using Cinema.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Interfaces
{
    public interface IBookingFoodRepository : IBaseRepository<BookingFood>
    {
        public Task<IEnumerable<BookingFood>> getByIdsAndBookingId(Guid bookingId, List<Guid> BookingFoodId);
        public Task AddRange(IEnumerable<BookingFood> bookingFoods);
    }
}
