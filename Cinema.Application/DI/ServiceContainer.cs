using Cinema.Application.AutoMappers;
using Cinema.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.DI
{
    public static class ServiceContainer 
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services ) 
        {
           
            services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }
    }
}
