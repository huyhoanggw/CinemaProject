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
                await PermissionHelper.AddPermissionToRoleAsync(dbcontext, "4bc8e6af-9e55-4116-b1a8-20960bebb8bd", permission.Code);

            }
        }
    }
}
