
using Cinema.Application.Interfaces.Hubs;
using Cinema.Domain.Enitities;
using Cinema.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.SignalR.Services
{
    public class SeatNotificationService(IHubContext<SeatHub> context) : ISeatNotificationService
    {
        public async Task SeatStatusChanged(Guid showtimeId, Guid SeatId, ShowtimeSeatStatus status, DateTime holdUntil)
        {
            await context.Clients.Group(showtimeId.ToString()).SendAsync("SeatsStatusChanged", new
            {
                showtimeId,
                SeatId,
                status,
                holdUntil
            });
        }
    }
}
