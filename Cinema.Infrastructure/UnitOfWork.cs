using Cinema.Application.Interfaces;
using Cinema.Infrastructure.Database;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure
{
    public class UnitOfWork(CinemaDbcontext dbcontext) : IUnitOfWork
    {
        public async Task BeginTransaction(CancellationToken cancellationToken = default)
        {
            await dbcontext.Database.BeginTransactionAsync(cancellationToken);   
        }

        public async Task CommitTransaction(CancellationToken cancellationToken = default)
        {
             await dbcontext.Database.CommitTransactionAsync(cancellationToken);
        }

        public async Task RollbackTransaction(CancellationToken cancellationToken = default)
        {
            await dbcontext.Database.RollbackTransactionAsync(cancellationToken);   
        }

        public async Task<int> SaveChangeAsync(CancellationToken cancellationtoken = default)
        {
            return await dbcontext.SaveChangesAsync(cancellationtoken);
                
        }
    }
}
