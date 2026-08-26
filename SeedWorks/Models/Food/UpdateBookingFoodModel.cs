using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedWorks.Models.Food
{
    public record UpdateBookingFoodModel
    {
       public  Cinema.Domain.Enitities.Food Food{get;set;}
       public int Quanlity{get;set;}
        public decimal UnitPrice { get; set; }
    }
}
