using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedWorks.Models.Theater
{
    public record UpdateTheaterModel(
            Guid Id ,
            string Name
        );
    
}
