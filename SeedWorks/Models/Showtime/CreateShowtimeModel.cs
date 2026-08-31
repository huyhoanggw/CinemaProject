using Cinema.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedWorks.Models.Showtime
{
    public record CreateShowtimeModel
    {
        public Guid MovieId{get;set;}


      public Guid TheaterId{get;set;}
      public DateTime StartTime {get;set;}

         public DateTime EndTime{get;set;}

      public decimal BasePrice{get;set;}

      public ShowtimeStatus Status {get;set;}

       public ICollection<Guid> ShowtimeSeatIds{get;set;}
       public ICollection<Guid> BookingIds {get;set;}
    }




}
