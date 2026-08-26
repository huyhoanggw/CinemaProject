using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedWorks.Models.Food
{
    public record CreateBookingFoodModel
    {
        public Guid FoodId { get; set; }
         public int Quanlity { get; set; }
     } 
      
           
    
}
