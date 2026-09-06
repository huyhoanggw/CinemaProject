using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Contracts.Models.Payment
{
    public record PaymentGatewayResult
    (
        bool success,
        string? TransactionId ,
        string? PaymentUrl ,
        string? message
    );


    
}
