using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedWorks.Models.Food
{
    public record UpdateFoodModel(
         string Name,
          decimal Price,
          int Quanlity
        );
   }
