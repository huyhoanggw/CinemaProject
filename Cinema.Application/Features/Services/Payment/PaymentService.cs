using Cinema.Application.Features.Services.PaymentService;
using Cinema.Application.Interfaces;
using Cinema.Domain.Enitities;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Payment;
using SeedWorks.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Services.Payment
{
    public class PaymentService(IBookingRepository bookingRepository , IPaymentRepository paymentRepository 
        , IUnitOfWork unitOfwork , IEnumerable<IPaymentGateway> gateways) : IPaymentService
    {
        public async Task<PaymentResult> CreatePaymentAsync(Guid BookingId, PaymentMethod paymentMethod, string ReturnUrl, string clientIp, CancellationToken cancellationToken)
        {
            var booking = await bookingRepository.FindByIdAsync(BookingId);
            if (booking is null) throw new Exception("Booking not found");
            if (booking.Status != BookingStatus.Pending) throw new Exception("booking cannot be paid");
            var gateway = gateways.FirstOrDefault(x => x.PaymentMethod == paymentMethod);
            if (gateway is null) throw new Exception("Payment method is not supported");
            var payment = new Domain.Enitities.Payment()
            {
                Id = Guid.NewGuid(),
                BookingId = booking.Id,
                Amount = booking.TotalPrice,
                PaymentMethod = gateway.PaymentMethod,
                Status = PaymentStatus.Pending,
                CreateAt = DateTime.UtcNow
            };
            await paymentRepository.CreateAsync(payment);
            var gatewayRequest = new PaymentGatewayRequest(payment.Id, booking.BookingCode, payment.Amount, ReturnUrl,clientIp);
            var result = await gateway.CreatePaymentAsync(gatewayRequest, cancellationToken);
            if (!result.success)
            {
                payment.Status = PaymentStatus.Failed;
                await unitOfwork.SaveChangeAsync(cancellationToken);
                throw new Exception(result.message);
            }
            payment.PaymentUrl = result.PaymentUrl;
            payment.TransactionId = result.TransactionId;
            await unitOfwork.SaveChangeAsync(cancellationToken);
            return new PaymentResult()
            {
                paymentUrl = payment.PaymentUrl,
                PaymentId = payment.Id,
                Status = payment.Status.ToString()
            };
           
        }

        public async Task<bool> HandlerPaymentCallback(SortedDictionary<string,string> parameters, CancellationToken cancellationToken)
        {
            if (!parameters.TryGetValue("vnp_TxnRef", out var orderCode))
            {
                return false;
            }
            var payment = await paymentRepository.GetByBookingCode(orderCode);
            if (payment is null) return false;
            var gateway = gateways.First(x => x.PaymentMethod == payment.PaymentMethod);
            var result =await  gateway.VerifyPaymentAsync(parameters, cancellationToken);
            if (!result.success)
            {
                payment.Status = PaymentStatus.Failed;
                await unitOfwork.SaveChangeAsync(cancellationToken);
                return false;
            }
            payment.Status = PaymentStatus.Success;
            payment.TransactionId = result.TransactionId;
            payment.PaidAt = DateTime.UtcNow;
            payment.Booking.Status = BookingStatus.Confirmed;
            await unitOfwork.SaveChangeAsync(cancellationToken);
            return true;
        }

        public async Task<PaymentReturnDto> HandlerPaymentReturn(SortedDictionary<string, string> parameters, CancellationToken cancellationToken)
        { 
             if (!parameters.TryGetValue(
           "vnp_TxnRef",
             out var bookingCode))
            {
                return new PaymentReturnDto(false,null,null);
            }
             var payment = await paymentRepository.GetByBookingCode(bookingCode);
            if (payment is null) return new PaymentReturnDto(false, null, null);
            var gateway = gateways.First(x => x.PaymentMethod == payment.PaymentMethod);
            if (gateway is null)
            {
                return new PaymentReturnDto(
                    false,
                    bookingCode,
                    payment.PaymentMethod);
            }
            var result = await gateway.VerifyPaymentAsync(parameters, cancellationToken);
            if(result.success)
            {
                return new PaymentReturnDto(true, bookingCode, payment.PaymentMethod);
            }
            return new PaymentReturnDto(false, bookingCode, payment.PaymentMethod);
        }
    }
}
