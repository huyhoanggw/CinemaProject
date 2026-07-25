using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Cinema.IdentityApi.Database.Factories
{
    public class ConfigurationDbcontextFactory : IDesignTimeDbContextFactory<ConfigurationDbContext>
    {
        public ConfigurationDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            var optionsbuilder = new DbContextOptionsBuilder<ConfigurationDbContext>();
            optionsbuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"), sql => sql.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));
            var storeOptions = new ConfigurationStoreOptions();
            return new ConfigurationDbContext(optionsbuilder.Options) { StoreOptions = storeOptions };
        }
    }
}
