using Cinema.Domain.Enitities;
using SeedWorks.Models.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<PaymentResult> CreatePaymentAsync(Guid BookingId, PaymentMethod paymentMethod
            , string ReturnUrl, string clientIp, CancellationToken cancellationToken);
        Task<bool> HandlerPaymentCallback(SortedDictionary<string,string> callback , CancellationToken cancellationToken);
        Task<PaymentReturnDto> HandlerPaymentReturn(SortedDictionary<string, string> parameters, CancellationToken cancellationToken);

    }
}
