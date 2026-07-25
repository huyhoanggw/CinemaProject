using Cenima.IdentityApi.Database;
using Cenima.IdentityApi.Database.Models;
using Microsoft.AspNetCore.Identity;

namespace Cinema.IdentityApi.Database.Configuration
{
    public class ApplicationDbcontextSeed
    {
        private readonly IPasswordHasher<ApplicationUser> passwordHash = new PasswordHasher<ApplicationUser>();
        public async Task SeedAsync(ApplicationDbcontext context, ILogger<ApplicationDbcontextSeed> logger, int? retry = 0)
        {
            if (retry is not null)
            {
                if (!context.Users.Any())
                {
                    try
                    {
                        await context.AddRangeAsync(GetDefaultUsers());
                    }
                    catch (Exception ex)
                    {
                        if (retry < 10)
                        {
                            logger.LogError("Error Occured while migrating");
                            retry++;
                            await SeedAsync(context, logger, retry);
                        }

                    }
                }
            }
        }
        private IEnumerable<ApplicationUser> GetDefaultUsers()
        {
            var user = new ApplicationUser()
            {
                Email = "admin@gmail.com",
                Id = Guid.NewGuid().ToString(),
                LastName = "Account",
                FirstName = "Admin",
                PhoneNumber = "1234567890",
                UserName = "admin@gmail.com",
                NormalizedEmail = "ADMIN@DEMO.COM",
                NormalizedUserName = "ADMIN@DEMO.COM",
                SecurityStamp = Guid.NewGuid().ToString("D"),

            };
            user.PasswordHash = passwordHash.HashPassword(user, "admin123@");
            return new List<ApplicationUser> {
           user
            };
        }
    }
}
