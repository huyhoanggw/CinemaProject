using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedWorks.Models.Payment
{
    public record PaymentGatewayCallback
    {
        public string OrderCode { get; set; }
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string ReponseCode { get; set; }
        public string TransactionStatus { get; set; }
        public string SecureHash { get; set; }
    }
}
