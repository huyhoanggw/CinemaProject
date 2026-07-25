using Microsoft.AspNetCore.Identity;

namespace Cenima.IdentityApi.Database.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
