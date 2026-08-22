using Cinema.IdentityApi.Database;
using Cinema.IdentityApi.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cinema.IdentityApi.Helpers
{
    public static class PermissionHelper
    {
        public async static Task AddPermissionToRoleAsync(
            ApplicationDbcontext dbcontext , 
            string RoleId ,
            string PermissionCode)
        {
            var permission = await dbcontext.Set<Permission>().Where(x=> x.Code == PermissionCode).FirstOrDefaultAsync();
            var exists = await dbcontext.Set<RolePermissions>().AnyAsync(x => x.RoleId == RoleId && x.PermissionId == permission.Id);
            if(!exists)
            {
               await dbcontext.Set<RolePermissions>().AddAsync(new RolePermissions()
                {
                    PermissionId = permission.Id ,
                    RoleId = RoleId
                });
                await dbcontext.SaveChangesAsync();
             }
        }
    }
}
