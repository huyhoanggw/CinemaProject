using Cinema.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Interfaces.Hubs
{
    public interface ISeatNotificationService
    {
        Task SeatStatusChanged(Guid showtimeId, Guid SeatId,ShowtimeSeatStatus status , DateTime holdUntil);
    }
}
