using Cenima.IdentityApi.Database.Models;
using Cinema.IdentityApi.Interfaces;
using Duende.IdentityModel;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Cinema.IdentityApi.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPermissionServices _permissionService;

        public ProfileService(UserManager<ApplicationUser> userManager , IPermissionServices PermissionServices)
        {
            _userManager = userManager;
            _permissionService = PermissionServices;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var userId = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) return;
            var UserPermissions = await _permissionService.GetUserPermissionsAsync(userId);
            var claims = new List<Claim>
            {
                new(JwtClaimTypes.Subject , user.Id),
               new(JwtClaimTypes.Name, user.UserName ?? ""),
            new(JwtClaimTypes.Email, user.Email ?? ""),
            new("first_name", user.FirstName ?? ""),
            new("last_name", user.LastName ?? ""),
                        };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, role));
            }
            foreach(var permission in UserPermissions)
            {
                claims.Add(new("permission",permission));
            }
            context.IssuedClaims.AddRange(claims);

        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var userId = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(userId);
            context.IsActive = user != null;
        }
    }
}
