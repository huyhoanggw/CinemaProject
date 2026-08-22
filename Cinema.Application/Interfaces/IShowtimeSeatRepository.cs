using Cinema.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Interfaces
{
    public interface IShowtimeSeatRepository : IBaseRepository<ShowtimeSeat>
    {
        Task<List<ShowtimeSeat>> GetByShowtimeAndSeatIdsAsync(Guid showtimeId, IEnumerable<Guid> seatId);
        Task<List<ShowtimeSeat>> GetByIds( IEnumerable<Guid> ShowtimeSeatIds);
    }
}
