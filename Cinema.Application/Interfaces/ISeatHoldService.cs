using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Interfaces
{
    public interface ISeatHoldService
    {
        Task<bool> HoldSeatAsync(
             Guid showtimeId,
             Guid seatId,
             string userId,
             TimeSpan duration);
        Task<bool> HoldSeatsAsync(
             Guid showtimeId,
             List<Guid> seatIds,
             string userId,
             TimeSpan duration);

        Task<bool> IsSeatHeldAsync(
            Guid showtimeId,
            Guid seatId);

        Task<string?> GetSeatHolderAsync(
            Guid showtimeId,
            Guid seatId);

        Task ReleaseSeatsAsync(
            Guid showtimeId,
            List<Guid> seatIds);
    }
}
