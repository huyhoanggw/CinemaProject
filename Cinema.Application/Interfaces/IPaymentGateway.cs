using Cinema.Domain.Enitities;
using SeedWorks.Models.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Interfaces
{
    public interface IPaymentGateway
    {
        PaymentMethod PaymentMethod { get; }
        Task<PaymentGatewayResult> CreatePaymentAsync(PaymentGatewayRequest request , CancellationToken cancellationToken);
        Task<PaymentGatewayResult> VerifyPaymentAsync(SortedDictionary<string, string>  request , CancellationToken cancellationToken);
        Task<bool> ValidateSignature(SortedDictionary<string, string> parameters, CancellationToken cancellationToken);
    }
}
