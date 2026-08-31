using Cinema.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedWorks.Models.Payment
{
    public record PaymentReturnDto
        (
            bool success,
            string? bookingCode , 
            PaymentMethod? PaymentMethod
        );
    
    
}
