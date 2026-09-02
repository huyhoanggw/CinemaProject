using Cinema.Application.Interfaces.Hubs;
using Cinema.SignalR.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.SignalR.DI
{
    public static class ServicesContainer
    {
        public static IServiceCollection AddSignalRService(this IServiceCollection services)
        {
            services.AddScoped<ISeatNotificationService, SeatNotificationService>();
            return services;
        }
    }
}
