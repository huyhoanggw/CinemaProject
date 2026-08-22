using Cenima.IdentityApi.Database;
using Cenima.IdentityApi.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
                        var users = GetDefaultUsers();
                        await context.AddRangeAsync(users);
                        var adminRole =await context.Roles.FirstOrDefaultAsync(x => x.Name == "Admin");
                        if (adminRole == null)
                        {
                            throw new Exception("Admin role does not exist.");
                        }
                        foreach(var user in users)
                        {
                           await context.UserRoles.AddAsync(new IdentityUserRole<string>()
                            {
                                RoleId = adminRole.Id,
                                UserId = user.Id,
                            });
                        }
                        await context.SaveChangesAsync();
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
                NormalizedEmail = "ADMIN@GMAIL.COM",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString("D")
                 
            };
            user.PasswordHash = passwordHash.HashPassword(user, "admin123@");
            return new List<ApplicationUser> {
           user
            };
        }
    }
}
