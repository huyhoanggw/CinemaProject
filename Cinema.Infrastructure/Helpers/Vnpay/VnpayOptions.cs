using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Helpers.Vnpay
{
    public class VnpayOptions
    {
        public string vnp_TmnCode { get; set; } = default!;
        public string vnp_HashSecret { get; set; } = default!;
        public string vnp_Url { get; set; } = default!;
        public string ReturnUrl { get; set; } = default!;


    }
}
