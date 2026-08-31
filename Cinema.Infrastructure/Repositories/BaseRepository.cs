using Cinema.Application.Interfaces;
using Cinema.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SeedWorks.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Repositories
{
    public class BaseRepository<T>(CinemaDbcontext _context , ILogger<BaseRepository<T>> _logger) : IBaseRepository<T> where T : class
    {
        public async Task<IEnumerable<T>> CreateRangeAsync(List<T> entities)
        {
             await _context.AddRangeAsync(entities);
            return entities;
        }

        public async Task<T> CreateAsync(T entity)
        {
            
            try
            {
                await _context.AddAsync(entity);
               

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var obj = await FindByIdAsync(Id);    
            if(obj is  null)
            {
                return false;
            }
            try
            {
                _context.Remove(obj);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return true;
        }

        public async Task<T?> FindByIdAsync(Guid Id)
        {
            var result = await _context.Set<T>().FindAsync(Id);
            if (result is null) return null;
            return result;
                
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var list = await _context.Set<T>().ToListAsync();
            return list;
        }

        public async Task<T?> GetByAsync(Expression<Func<T, bool>> predicate)
        {
                var result = await  _context.Set<T>().Where(predicate).FirstOrDefaultAsync();
            if (result is null) return null;
            return result;
        }

        public async Task<int> GetCountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            var result = predicate == null ? await _context.Set<T>().CountAsync() : await _context.Set<T>().Where(predicate).CountAsync();
            return result;
        }

        public async Task<PagedResult<T>> GetPagingAsync(  int PageIndex, int PageSize, Expression<Func<T, bool>>? firstPredicate = null
            , Expression<Func<T, bool>>? secondPredicate = null)
        {
            IQueryable<T> filter = _context.Set<T>();
               if(firstPredicate != null)
            {
                filter = filter.Where(firstPredicate);
            }
               if(secondPredicate != null)
            {
                filter = filter.Where(secondPredicate);
            }
               var itemCount = await filter.CountAsync();
            var items = await filter.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToListAsync();
            return new PagedResult<T>() { CurrentPage = PageIndex , PageSize = PageSize , Items = items
                , TotalCount = itemCount , TotalPage = (int)Math.Ceiling((double)itemCount / PageSize)};
        }

        public async Task UpdateAsync(T entity)
        {
                _context.Update(entity);
        }
    }
}
