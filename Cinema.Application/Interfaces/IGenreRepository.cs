using Cinema.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Interfaces
{
    public interface IGenreRepository : IBaseRepository<Genre>
    {
        public Task<List<Genre>> GetGenresByIds(List<Guid> Ids);
    }
}
