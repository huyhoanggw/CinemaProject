using Cenima.IdentityApi.Database.Models;
using Cinema.IdentityApi.Database.Configuration;
using Cinema.IdentityApi.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security;

namespace Cinema.IdentityApi.Database
{
    public class ApplicationDbcontext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
            base.OnModelCreating(builder);

            builder.Entity<Permission>().HasKey(x => x.Id);
            builder.Entity<RolePermissions>(entity =>
            {
                entity.HasKey(x => new { x.RoleId, x.PermissionId });
                entity.HasOne(x => x.permission).WithMany(x => x.RolePermissions).HasForeignKey(x => x.PermissionId);
                entity.HasOne<IdentityRole>().WithMany().HasForeignKey(x => x.RoleId);
            });
        }
     public   DbSet<Permission> Permissions { get; set; }
      public  DbSet<RolePermissions> RolePermissions { get; set; }

    }
}
