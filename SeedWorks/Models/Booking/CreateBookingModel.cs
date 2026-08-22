using Cinema.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedWorks.Models.Booking
{
    public record CreateBookingModel(
          string UserId ,
          Guid ShowtimeId ,
         Cinema.Domain.Enitities.Showtime Showtime ,
          string BookingCode ,
          decimal TotalPrice ,
          BookingStatus Status ,
          ICollection<BookingSeat> BookingSeats ,
          ICollection<BookingFood> BookingFoods ,
          Payment? Payment 
        );
    
    
}
