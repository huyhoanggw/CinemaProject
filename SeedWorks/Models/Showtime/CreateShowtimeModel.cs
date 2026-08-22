using Cinema.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedWorks.Models.Showtime
{
    public record CreateShowtimeModel(


           
             Cinema.Domain.Enitities.Movie Movie , 

             
              Cinema.Domain.Enitities.Theater Theater , 
              DateTime StartTime ,

             DateTime EndTime ,

              decimal BasePrice ,

             ShowtimeStatus Status ,

              ICollection<ShowtimeSeat> ShowtimeSeats    ,  
              ICollection<Cinema.Domain.Enitities.Booking> Bookings   
            );
    
    
}
