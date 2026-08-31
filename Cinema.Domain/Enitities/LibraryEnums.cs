using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Domain.Enitities
{
   
    public enum ShowtimeStatus
    {
        Open,
        Closed,
        Cancelled
    }

    public enum ShowtimeSeatStatus
    {
        Available,
        Hold,
        Booked
    }

    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Expired
    }

    public enum PaymentStatus
    {
        Pending,
        Success,
        Failed,
        Refunded
    }

    public enum PaymentMethod
    {
        VnPay,
        Momo,
        ZaloPay
        
    }
}
