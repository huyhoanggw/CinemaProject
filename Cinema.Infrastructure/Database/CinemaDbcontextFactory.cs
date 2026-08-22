using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Database
{
    internal class CinemaDbcontextFactory : IDesignTimeDbContextFactory<CinemaDbcontext>
    {
        public CinemaDbcontext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            var option = new DbContextOptionsBuilder();
            option.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            return new CinemaDbcontext(option.Options);
        }
    }
}
