using Cinema.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedWorks.Models.Showtime
{
   public  record UpdateShowtimeModel(
    
    Cinema.Domain.Enitities.Movie Movie , 
     Guid TheaterId ,

     Cinema.Domain.Enitities.Theater Theater , 
     DateTime StartTime ,

     DateTime EndTime ,

     decimal BasePrice ,

     ShowtimeStatus Status 
        );
    }
