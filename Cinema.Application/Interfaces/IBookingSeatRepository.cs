using Cinema.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Interfaces
{
    public interface IBookingSeatRepository : IBaseRepository<BookingSeat>
    {
        Task<IEnumerable< BookingSeat>> GetBookingSeatByIds(List<Guid> Id);
        public Task AddRange(IEnumerable<BookingSeat> bookingSeats);
    }
}
