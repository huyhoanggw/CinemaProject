using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public Task<int> SaveChangeAsync(CancellationToken cancellationtoken = default);
        public Task BeginTransaction(CancellationToken cancellationToken = default);
        public Task CommitTransaction(CancellationToken cancellationToken = default);
        public Task RollbackTransaction(CancellationToken cancellationToken = default);
    }
}
