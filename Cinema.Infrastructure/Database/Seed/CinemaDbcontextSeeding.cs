using Bogus;
using Cinema.Domain.Enitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Database.Seed
{
    public static class CinemaDbcontextSeeding
    {
        public static async Task  SeedAsync(CinemaDbcontext dbcontext , int? retry = 1 )
        {
            if(retry >= 0)
            {
                try
                {
                    if (!dbcontext.Set<Genre>().Any())
                    {
                        dbcontext.Set<Genre>().AddRange(await SeedGenres());
                        await dbcontext.SaveChangesAsync();
                    }
                    if (!dbcontext.Set<Movie>().Any())
                    {
                        dbcontext.Set<Movie>().AddRange(await SeedMovies(dbcontext));
                        await dbcontext.SaveChangesAsync();
                    }
                    if (!dbcontext.Set<Seat>().Any())
                    {
                        dbcontext.Set<Seat>().AddRange(await SeedSeats(dbcontext));
                        await dbcontext.SaveChangesAsync();
                    }
                    if (!dbcontext.Set<Theater>().Any())
                    {
                        dbcontext.Set<Theater>().AddRange(await SeedTheaters(dbcontext));
                        await dbcontext.SaveChangesAsync();
                    }
                   
                    if (!dbcontext.Set<Showtime>().Any())
                    {
                        dbcontext.Set<Showtime>().AddRange(await SeedShowtimes(dbcontext));
                        await dbcontext.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    if (retry == 0)
                        throw;
                    await SeedAsync(dbcontext, retry-1);
                }
                
            }
        }

        private static async Task<List<Showtime>> SeedShowtimes(CinemaDbcontext dbcontext)
        { 
                var movies  = await dbcontext.Set<Movie>().ToListAsync();
                var theaters  = await dbcontext.Set<Theater>().ToListAsync();
            var faker = new Faker<Showtime>()
         .RuleFor(x => x.Id, f => Guid.NewGuid())
         .RuleFor(x => x.MovieId, f => f.PickRandom(movies).Id)              
         .RuleFor(x => x.TheaterId, f => f.PickRandom(theaters).Id)
         .RuleFor(x => x.StartTime, f => DateTime.UtcNow)
         .RuleFor(x => x.EndTime, f => DateTime.UtcNow.AddHours(2))
         .RuleFor(x => x.BasePrice, f => f.Random.Number(50, 500) * 1000m)         
         .RuleFor(x => x.CreateAt, f => DateTime.UtcNow);
            var showtimes = faker.Generate(5);
            return showtimes;
        }

        private static async Task<List<Theater>> SeedTheaters(CinemaDbcontext _context)
        {
            var seats = await _context.Set<Seat>().ToListAsync(); 
            var theaters = new List<Theater>();
            var chars = new List<string>()
            {
                "A",
                "B",
                "C",
                "D"
            };
            foreach (var c in chars) 
            {
                for(int i = 1; i <= 3; i++)
                {
                    theaters.Add(new Theater()
                    {
                        Id = Guid.NewGuid(),
                        Name = $"{c}{i}",
                        Seats = seats,
                        CreateAt = DateTime.UtcNow
                    });

                }
            }
            return theaters;
        }

        private static async Task<List<Seat>> SeedSeats(CinemaDbcontext dbcontext)
        {
            var Seats = new List<Seat>();
            var theaters = await dbcontext.Set<Theater>().ToListAsync();
            var chars = new List<string>()
            {
                "A",
                "B",
                "C",
                "D",
                "E",
                "F",
                "G",
                "H"
            };
            foreach(var theater in theaters)
            {
                foreach (var c in chars)
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        Seats.Add(new Seat()
                        {
                            Id = Guid.NewGuid(),
                            Number = i,
                            Row = c,
                            CreateAt = DateTime.UtcNow,
                            TheaterId = theater.Id
                        }
                        );
                    }

                }
            }
          
            return Seats;
        }

        public async static Task<List<Movie>> SeedMovies(CinemaDbcontext dbcontext)
        {   
         
            var genres = await dbcontext.Set<Genre>().ToListAsync();
           
            var movies = new List<Movie>()
            {
                new Movie()
                {
                    Id = Guid.NewGuid(),
                    Name = "harry potter 1 ",
                    Description = "Description",
                    CreateAt = DateTime.UtcNow
                   

                },

                new Movie()
                {
                    Id = Guid.NewGuid(),
                    Name = "harry potter 2 ",
                    Description = "Description",
                    CreateAt = DateTime.UtcNow


                },


                new Movie()
                {
                    Id = Guid.NewGuid(),
                    Name = "harry potter 3 ",
                    Description = "Description",
                    CreateAt = DateTime.UtcNow


                },


                new Movie()
                {
                    Id = Guid.NewGuid(),
                    Name = "harry potter 4 ",
                    Description = "Description",
                    CreateAt = DateTime.UtcNow


                }
            };
            var movieGenres = new List<MovieGenre>();
            foreach(var movie in movies)
            {
                foreach(var genre in genres)
                {
                     movieGenres.Add(new MovieGenre()
                    {
                        GenreId = genre.Id,
                        MovieId = movie.Id
                    });
                }
               movie.MovieGenre = movieGenres;
                movieGenres.Clear();
            }
            return movies;           
        } 
            
        public async static Task<List<Genre>> SeedGenres()
        {
            return new List<Genre>()
            {
                new Genre()
                {
                    Id = Guid.NewGuid(),
                    Name = "Tình cảm",
                    CreateAt = DateTime.UtcNow
                    
                },
                new Genre()
                {
                    Id = Guid.NewGuid(),
                    Name = "Kinh dị",
                    CreateAt = DateTime.UtcNow
                    
                },
                new Genre()
                {
                    Id = Guid.NewGuid(),
                    Name = "Drama",
                    CreateAt = DateTime.UtcNow
                    
                },
                new Genre()
                {
                    Id = Guid.NewGuid(),
                    Name = "Phép thuật",
                    CreateAt = DateTime.UtcNow
                    
                },
                new Genre()
                {
                    Id = Guid.NewGuid(),
                    Name = "Hài hước",
                    CreateAt = DateTime.UtcNow
                    
                }
            }; 
        }
    }
}
