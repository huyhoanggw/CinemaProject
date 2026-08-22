using Cinema.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.BackgroundServices
{
    public class BookingExprationService(IServiceScopeFactory _scopeFactory , ILogger<BookingExprationService> logger) : BackgroundService
    {
        protected async  override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            try
            {
                using var scope = _scopeFactory.CreateScope();

                var expirationService =
                    scope.ServiceProvider
                        .GetRequiredService<IBookingExpirationService>();

                await expirationService.ExpireAsync(
                    stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "Error while expiring bookings");
            }
            await Task.Delay(
              TimeSpan.FromSeconds(20),
              stoppingToken);
            }
    }
}
