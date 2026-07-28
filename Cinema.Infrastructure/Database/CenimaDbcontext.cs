using Cinema.Domain.Enitities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Database
{
    public class CenimaDbcontext : DbContext
    {
        public CenimaDbcontext(DbContextOptions options) : base(options)
        {
        }

        protected CenimaDbcontext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); base.OnModelCreating(builder);

            builder.Entity<MovieGenre>()
                .HasKey(x => new { x.MovieId, x.GenreId });

            builder.Entity<BookingSeat>()
                .HasKey(x => new { x.BookingId, x.ShowtimeSeatId });

            builder.Entity<Payment>()
                .HasOne(x => x.Booking)
                .WithOne(x => x.Payment)
                .HasForeignKey<Payment>(x => x.BookingId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ShowtimeSeat>()
                .HasIndex(x => new { x.ShowtimeId, x.SeatId })
                .IsUnique();

            builder.Entity<ShowtimeSeat>()
                .HasOne(x => x.Showtime)
                .WithMany(x => x.ShowtimeSeats)
                .HasForeignKey(x => x.ShowtimeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ShowtimeSeat>()
                .HasOne(x => x.Seat)
                .WithMany(x => x.ShowtimeSeats)
                .HasForeignKey(x => x.SeatId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<BookingSeat>()
                .HasOne(x => x.Booking)
                .WithMany(x => x.BookingSeats)
                .HasForeignKey(x => x.BookingId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<BookingSeat>()
                .HasOne(x => x.ShowtimeSeat)
                .WithMany(x => x.BookingSeats)
                .HasForeignKey(x => x.ShowtimeSeatId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Booking>()
                .HasOne(x => x.Showtime)
                .WithMany(x => x.Bookings)
                .HasForeignKey(x => x.ShowtimeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<BookingFood>()
        .HasKey(x => new { x.BookingId, x.FoodId });
        }
        DbSet<Booking> Booking {  get; set; }
        DbSet<BookingSeat> BookingSeat {  get; set; }
        DbSet<Food> Food {  get; set; }
        DbSet<Genre> Genre {  get; set; }
        DbSet<Movie> Movie {  get; set; }
        DbSet<MovieGenre> MovieGenre {  get; set; }
        DbSet<Payment> Payment {  get; set; }
        DbSet<Seat> Seat {  get; set; }
        DbSet<Showtime> Showtime {  get; set; }
        DbSet<ShowtimeSeat> ShowtimeSeat {  get; set; }
        DbSet<Theater> Theater {  get; set; }
        DbSet<BookingFood> BookingFood {  get; set; }
          }
}
