using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.IdentityApi.Database.Entities
{
    public class RolePermissions
    {
        public string RoleId { get; set; }
        public int PermissionId { get; set; }
        public Permission permission { get; set; }

    }
}
