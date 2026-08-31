using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedWorks.Models.Payment
{
    public record PaymentResult
    {
      public  Guid? PaymentId { get; set; }
        public string? paymentUrl { get; set; }
        public string? Status { get; set; }
    }
}
