using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Interfaces
{
    public interface IBookingExpirationService
    {
       public Task ExpireAsync(CancellationToken cancellationToken);
    }
}
