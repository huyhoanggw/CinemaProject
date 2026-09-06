using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Contracts.Models.Seat
{
    public record CreateBookingSeatModel
    {
         public Guid SeatId {  get; set; }

 }

}
