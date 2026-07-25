using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Cinema.IdentityApi.Database.Factories
{
    public class PersistedGrantDbcontextFactory : IDesignTimeDbContextFactory<PersistedGrantDbContext>
    {
        public PersistedGrantDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            var optionsbuilder = new DbContextOptionsBuilder<PersistedGrantDbContext>();
            optionsbuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"), sql => sql.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));
            var storeOptions = new OperationalStoreOptions();
            return new PersistedGrantDbContext(optionsbuilder.Options) { StoreOptions = storeOptions };
        }
    }
}
