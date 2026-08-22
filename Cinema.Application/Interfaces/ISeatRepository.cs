using Cinema.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Interfaces
{
    public interface ISeatRepository : IBaseRepository<Seat>
    {
        public Task<IEnumerable<Seat>> GetSeatsByIds(List<Guid> Ids);
    }
}
