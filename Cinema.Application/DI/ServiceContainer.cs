using Cinema.Application.AutoMappers;
using Cinema.Application.BackgroundServices;
using Cinema.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Cinema.Application.Features.Services.PaymentService;
using Cinema.Application.Features.Services.Payment;
using Cinema.Application.Interfaces.Hubs;

namespace Cinema.Application.DI
{
    public static class ServiceContainer 
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services ) 
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceContainer).Assembly));
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddHostedService<BookingExprationService>();
            services.AddScoped<IPaymentService, PaymentService>();
           services.AddHttpContextAccessor();

            return services;
        }
    }
}
