using Cinema.Application.Interfaces;
using Cinema.Application.Interfaces.Redis;
using Cinema.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Services.SeatHold
{
    public class SeatHoldService(IRedisServices redis) : ISeatHoldService
    {
        private static string BuildKey(Guid showtimeId, Guid SeatId)
        {
            return $"showtime:{showtimeId}:seat:{SeatId}:hold";
        }
        public async Task<bool> HoldSeatAsync(Guid showtimeId, Guid seatid, string userid, TimeSpan duration)
        {
            var key = BuildKey(showtimeId, seatid);
            return await redis.SetIfNotExistsAsync(key, userid, duration);
        }

        public async Task<bool> IsSeatHeldAsync(Guid showtimeId, Guid seatId)
        {
            var key = BuildKey(showtimeId, seatId);
            return await redis.ExistsAsync(key);
        }

        public async Task<string?> GetSeatHolderAsync(Guid showtimeId, Guid seatId)
        {
            var key = BuildKey(showtimeId, seatId);
            return await redis.GetAsync(key);
        }

        public async Task ReleaseSeatsAsync(Guid showtimeId, List<Guid> seatIds)
        {
            foreach (var seatId in seatIds)
            {
                var key = BuildKey(showtimeId, seatId);
                await redis.DeleteAsync(key);
            }

        }

        public async Task<bool> HoldSeatsAsync(Guid showtimeId, List<Guid> seatIds, string userId, TimeSpan duration)
        {
            var heldseat = new List<Guid>();
            foreach (var seatId in seatIds)
            {
                var key = BuildKey(showtimeId, seatId);
                var success = await redis.SetIfNotExistsAsync(key, userId, duration);
                if (!success)
                {
                    foreach (var seat in heldseat)
                    {
                        var deletekey = BuildKey(showtimeId, seat);
                        await redis.DeleteAsync(deletekey);
                    }
                    return false;
                }
                heldseat.Add(seatId);
            }

            return true;
        }
    }
}
