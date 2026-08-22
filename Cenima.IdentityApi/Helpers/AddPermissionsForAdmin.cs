using Cinema.IdentityApi.Database;
using Cinema.IdentityApi.Database.Entities;
using Cinema.IdentityApi.PermissionModule.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cinema.IdentityApi.Helpers
{
    public static class AddPermissionsForAdmin
    {
        public static async Task AddAsync(ApplicationDbcontext dbcontext )
        {
         
             var permissions = await dbcontext.Set<Permission>().ToListAsync();
            foreach(var permission in permissions)
            {
                await PermissionHelper.AddPermissionToRoleAsync(dbcontext, "c6bb5c19-8132-4076-8508-dcf9437afe0e", permission.Code);

            }
        }
    }
}
