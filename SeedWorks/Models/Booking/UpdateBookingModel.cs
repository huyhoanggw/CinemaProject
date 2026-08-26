using Cinema.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedWorks.Models.Booking
{
    public record UpdateBookingModel
    {
          public string UserId{get;set;}
          public Guid ShowtimeId{get;set;}
       public Cinema.Domain.Enitities.Showtime Showtime{get;set;}
        public string BookingCode{get;set;}
        public decimal TotalPrice{get;set;}
         public BookingStatus Status{get;set;}
         public ICollection<BookingSeat> BookingSeats{get;set;}
         public ICollection<BookingFood> BookingFoods{get;set;}
         public Payment? Payment { get; set; }
        }
        

        
}
