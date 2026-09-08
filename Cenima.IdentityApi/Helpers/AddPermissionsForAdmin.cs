using Cinema.IdentityApi.Database;
using Cinema.IdentityApi.Database.Entities;
using Cinema.IdentityApi.PermissionModule.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cinema.IdentityApi.Helpers
{
    public static class AddPermissionsForAdmin
    {
        public static async Task AddAsync(ApplicationDbcontext dbcontext)
        {

            var permissions = await dbcontext.Set<Permission>().ToListAsync();
            foreach (var permission in permissions)
            {
                await PermissionHelper.AddPermissionToRoleAsync(dbcontext, "31502c78-7a3f-4f5f-96b6-3504e1caee9a", permission.Code);

            }
        }
    }
}
