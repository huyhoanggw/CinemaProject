using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Cinema.IdentityApi.Database.Factories
{
    public class ApplicationDbcontextFactory : IDesignTimeDbContextFactory<ApplicationDbcontext>
    {
        public ApplicationDbcontext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            var optionsbuilder = new DbContextOptionsBuilder<ApplicationDbcontext>();
            optionsbuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            return new ApplicationDbcontext(optionsbuilder.Options);
        }
    }
}
