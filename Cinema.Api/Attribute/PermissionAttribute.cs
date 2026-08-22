using Microsoft.AspNetCore.Authorization;

namespace Cinema.Api.Attribute
{
    public class PermissionAttribute : AuthorizeAttribute
    {
        public PermissionAttribute(string permission)
        {
            Policy = permission;
        }
    }
}
