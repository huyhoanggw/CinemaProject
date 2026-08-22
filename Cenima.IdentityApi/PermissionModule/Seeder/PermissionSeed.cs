using Cinema.IdentityApi.Database;
using Cinema.IdentityApi.Database.Entities;
using Cinema.IdentityApi.PermissionModule.Constants;
using Microsoft.EntityFrameworkCore;

namespace Cinema.IdentityApi.PermissionModule.Seeder
{
    public static class PermissionSeed
    {
        public async static Task SeedAsync(ApplicationDbcontext dbcontext)
        {
            var permissions = GetListPermission();
            foreach (var permission in permissions)
            {
                var exists = await dbcontext.Set<Permission>().AnyAsync(x => x.Code == permission.Code);
                if (!exists) dbcontext.Set<Permission>().Add(permission);
            }

        }
        public static List<Permission> GetListPermission()
        {
            return new List<Permission>
            {
                new Permission
                {
                     Code = PermissionCodes.MovieRead,
                     Name = "Read Movie",
                     Description = "Can read movies"
                },
                 new Permission
                {
                     Code = PermissionCodes.MovieCreate,
                     Name = "Create Movie",
                     Description = "Can create movies"
                },
                   new Permission
                {
                      Code = PermissionCodes.MovieUpdate,
                     Name = "Update Movie",
                     Description = "Can update movies"
                },
                  new Permission
                {
                                         Code = PermissionCodes.MovieDelete,
                     Name = "Delete Movie",
                     Description = "Can delete movies"
                },
            // theater
                   new Permission
                {
                                         Code = PermissionCodes.TheaterRead,
                     Name = "Read Theater",
                     Description = "Can read theaters"
                },
                 new Permission
                {
                                          Code = PermissionCodes.TheaterCreate,
                     Name = "Create Theater",
                     Description = "Can create theaters"
                },
                   new Permission
                {
                                         Code = PermissionCodes.TheaterUpdate,
                     Name = "Update Theater",
                     Description = "Can update theaters"
                },
                  new Permission
                {
                                         Code = PermissionCodes.TheaterDelete,
                     Name = "Delete Theater",
                     Description = "Can delete theater"
                },
                  // seat
                        new Permission
                {
                                         Code = PermissionCodes.SeatRead,
                     Name = "Read Seat",
                     Description = "Can read seats"
                },
                 new Permission
                {
                                          Code = PermissionCodes.SeatCreate,
                     Name = "Create Seat",
                     Description = "Can create seats"
                },
                   new Permission
                {
                                         Code = PermissionCodes.SeatUpdate,
                     Name = "Update Seat",
                     Description = "Can update seats"
                },
                  new Permission
                {
                                         Code = PermissionCodes.SeatDelete,
                     Name = "Delete Seat",
                     Description = "Can delete Seat"
                },

                  // showtime 
                     new Permission
                {
                                          Code = PermissionCodes.ShowtimeRead,
                     Name = "Read Showtime",
                     Description = "Can read Showtimes"
                },
                 new Permission
                {
                                          Code = PermissionCodes.ShowtimeCreate,
                     Name = "Create Showtime",
                     Description = "Can create showtimes"
                },
                   new Permission
                {
                                         Code = PermissionCodes.ShowtimeUpdate,
                     Name = "Update Showtime",
                     Description = "Can update showtimes"
                },
                  new Permission
                {
                                          Code = PermissionCodes.ShowtimeDelete,
                     Name = "Delete Showtime",
                     Description = "Can delete showtimes"
                },
                  //food
                        new Permission
                {
                                         Code = PermissionCodes.FoodRead,
                     Name = "Read Food",
                     Description = "Can read foods"
                },
                 new Permission
                {
                                        Code = PermissionCodes.FoodCreate,
                     Name = "Create Food",
                     Description = "Can create foods"
                },
                   new Permission
                {
                                          Code = PermissionCodes.FoodUpdate,
                     Name = "Update Food",
                     Description = "Can update foods"
                },
                  new Permission
                {
                                          Code = PermissionCodes.FoodDelete,
                     Name = "Delete Food",
                     Description = "Can delete foods"
                },
                  // genre
                  new Permission
                {
                                          Code = PermissionCodes.GenreRead,
                     Name = "Read Genre",
                     Description = "Can read genres"
                },
                 new Permission
                {
                                          Code = PermissionCodes.GenreCreate,
                     Name = "Create Genre",
                     Description = "Can create genres"
                },
                   new Permission
                {
                                          Code = PermissionCodes.GenreUpdate,
                     Name = "Update Genre",
                     Description = "Can update genres"
                },
                  new Permission
                {

                     Code = PermissionCodes.GenreDelete,
                     Name = "Delete Genre",
                     Description = "Can delete genres"
                },
            };
        }
    }
}
