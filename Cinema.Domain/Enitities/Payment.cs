using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Domain.Enitities
{
    public class Payment : BaseEntity
    {
        public Guid BookingId { get; set; }

        public Booking Booking { get; set; } = default!;

        public decimal Amount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public PaymentStatus Status { get; set; }

        public string? TransactionId { get; set; }

        public DateTime? PaidAt { get; set; }
    }
}
