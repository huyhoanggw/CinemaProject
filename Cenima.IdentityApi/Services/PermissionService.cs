using Cinema.IdentityApi.Database;
using Cinema.IdentityApi.Database.Entities;
using Cinema.IdentityApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security;

namespace Cinema.IdentityApi.Services
{
    public class PermissionService(ApplicationDbcontext dbcontext) : IPermissionServices
    {
        public async Task<List<string>> GetUserPermissionsAsync(string userId)
        {
            return await dbcontext.UserRoles
           .Where(ur => ur.UserId == userId)
           .Join(
               dbcontext.RolePermissions,
               ur => ur.RoleId,
               rp => rp.RoleId,
               (ur, rp) => rp.PermissionId
           )
           .Join(
               dbcontext.Permissions,
               permissionId => permissionId,
               p => p.Id,
               (permissionId, p) => p.Code
           )
           .Distinct()
           .ToListAsync();
        }
        // dòng đầu là lọc điêu kiện lấy ra userrole với userid = UserId 
        //join dòng đầu là từ bảng userRoles as ur join với bảng rolePermission as rp sau khi cả 2 join thì lấy ra rp.permissionId 
        // join lần thứ 2 là lấy ra permissionId từ join dòng đầu và join với bảng Permissions as p sau khi join 2 PermissionId và bảng Permission thì lấy ra p.code
        // giống như select p.Code from Permissions as p where Permissions.Id = permissionId
    }
}
