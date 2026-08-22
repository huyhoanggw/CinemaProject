using Cinema.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedWorks.Models.Seat
{
    public record UpdateBookingSeatModel(
              ShowtimeSeat ShowtimeSeat, 
              decimal Price 
        );
   }
