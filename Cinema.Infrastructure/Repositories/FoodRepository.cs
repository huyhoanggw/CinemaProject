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
    public class FoodRepository : BaseRepository<Food>, IFoodRepository
    {
        private readonly CinemaDbcontext _context;
        private readonly ILogger<BaseRepository<Food>> _logger;

        public FoodRepository(CinemaDbcontext context, ILogger<BaseRepository<Food>> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Food>> getFoodByIds(List<Guid> Ids)
        {
          return await _context.Set<Food>().Where(x => Ids.Contains(x.Id)).ToListAsync();
        }
    }
}
