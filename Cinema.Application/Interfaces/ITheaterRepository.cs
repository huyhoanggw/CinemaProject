using Cinema.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Interfaces
{
    public interface ITheaterRepository : IBaseRepository<Theater>
    {
        //public Task<Theater> GetTheaterByIdAndShowtimeId(Guid theaterId, Guid showtimeId);
        public Task<Theater> GetTheaterByName(string name);
    }
}
