using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedWorks.Models.Payment
{
    public record PaymentGatewayRequest
    (
        Guid paymentId,
        string OrderCode ,
        decimal Amount ,
        string ReturnUrl,
        string clientIp
        );
    
}
