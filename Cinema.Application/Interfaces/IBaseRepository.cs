using Cinema.Domain.Enitities;
using SeedWorks.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> CreateRangeAsync(List<T> entity);
        Task UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid Id);
        Task<T?> FindByIdAsync(Guid Id);
                Task<IEnumerable<T>> GetAll();
        Task<T?> GetByAsync(Expression<Func<T,bool>> predicate);
        Task<int> GetCountAsync(Expression<Func<T,bool>>? predicate = null);
        Task<PagedResult<T>> GetPagingAsync( int PageIndex, int PageSize, Expression<Func<T, bool>>? firstPredicate = null
            , Expression<Func<T, bool>>? secondPredicate = null);

    }
}
