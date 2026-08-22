using Cinema.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Interfaces
{
    public interface IFoodRepository : IBaseRepository<Food>
    {
        public Task<IEnumerable<Food>> getFoodByIds(List<Guid> Ids);
    }
}
