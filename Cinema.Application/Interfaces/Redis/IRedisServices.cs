using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Interfaces.Redis
{
    public interface IRedisServices
    {
        Task<bool> SetAsync(string key, string value, TimeSpan? expiry = null);
        Task<bool> SetIfNotExistsAsync(string key, string value, TimeSpan? expiry = null);
        Task<string?> GetAsync(string key);
        Task<bool> DeleteAsync(string key);
        Task<bool> ExistsAsync(string key);
    }
}
