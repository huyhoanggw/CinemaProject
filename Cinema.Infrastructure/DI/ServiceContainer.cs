using Cinema.Application.Interfaces;
using Cinema.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.DI
{
    public static  class ServiceContainer 
    {
        public static IServiceCollection AddInfrastructureServie(this IServiceCollection services)
        {
            services.AddScoped<IBookingExpirationService, IBookingExpirationService>();
            services.AddScoped<IBookingFoodRepository, BookingFoodRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IBookingSeatRepository, BookingSeatRepository>();
            services.AddScoped<IFoodRepository,FoodRepository>();
            services.AddScoped<IGenreRepository,GenreRepository>();
            services.AddScoped<IMovieGenreRepository,MovieGenreRepository>();
            services.AddScoped<IMovieRepository,MovieRepository>();
            services.AddScoped<IPaymentRepository,PaymentRepository>();
            services.AddScoped<ISeatRepository,SeatRepository>();
            services.AddScoped<IShowtimeRepository,ShowtimeRepository>();
            services.AddScoped<IShowtimeSeatRepository,ShowtimeSeatRepository>();
            services.AddScoped<ITheaterRepository,TheaterRepository>();


            return services;
        }
    }
}
