using Cinema.Application.BackgroundServices;
using Cinema.Application.Interfaces;
using Cinema.Infrastructure.Database;
using Cinema.Infrastructure.Database.Seed;
using Cinema.Infrastructure.Helpers.PaymentGateway;
using Cinema.Infrastructure.Helpers.Vnpay;
using Cinema.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.DI
{
    public static  class ServiceContainer 
    {
        public static IServiceCollection AddInfrastructureServie(this IServiceCollection services , IConfiguration config)
        {
            services.AddScoped<IBookingExpirationService, BookingExpirationRepository>();
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<CinemaDbcontext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            services.Configure<VnpayOptions>(
            config.GetSection("VnpayOptions"));
            services.AddScoped<IPaymentGateway, VnpayPaymentGateway>();
            return services;
        }
        public async static Task<IApplicationBuilder> AddInfrastructurePolicies(this IApplicationBuilder app, ILogger logger)
        {
            using(var scope = app.ApplicationServices.CreateScope())
            {
                var service = scope.ServiceProvider;
                var dbcontext = service.GetRequiredService<CinemaDbcontext>();
                    await CinemaDbcontextSeeding.SeedAsync(dbcontext , 5);
                logger.LogInformation("completed seeding cinemaDbcontext");
            }
            return app; 
        }
    }
}
