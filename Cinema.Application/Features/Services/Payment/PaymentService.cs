using Cinema.Application.Interfaces;
using Cinema.Contracts.Models.Payment;
using Cinema.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Services.Payment
{
    public class PaymentService(IBookingRepository bookingRepository, IPaymentRepository paymentRepository
        , IUnitOfWork unitOfwork, IEnumerable<IPaymentGateway> gateways, IFoodRepository foodRepository, ISeatHoldService seatHoldService, IShowtimeSeatRepository showtimeSeatRepository) : IPaymentService
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
            var gatewayRequest = new PaymentGatewayRequest(payment.Id, booking.BookingCode, payment.Amount, ReturnUrl, clientIp);
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

        public async Task<bool> HandlerPaymentCallback(SortedDictionary<string, string> parameters, CancellationToken cancellationToken)
        {
            if (!parameters.TryGetValue("vnp_TxnRef", out var orderCode))
            {
                return false;
            }
            var payment = await paymentRepository.GetByBookingCode(orderCode);
            if (payment is null) return false;
            var gateway = gateways.First(x => x.PaymentMethod == payment.PaymentMethod);
            // lay bookingcode de tru so luong food
            var booking = await bookingRepository.GetByAsync(x => x.BookingCode.Equals(orderCode));
            if (booking is null) return false;
            // lay showtime seat de xoa key trong redis
            var showtimeseat = await showtimeSeatRepository.GetShowtimeSeatsByBookingSeats(booking.BookingSeats.ToList());
            if (showtimeseat is null) return false;
            if (booking.BookingFoods.Any())
            {
                var bookingsFood = booking.BookingFoods.ToList();
                var foods = await foodRepository.getFoodByIds(bookingsFood.Select(x => x.FoodId).ToList());
                foreach (var food in foods)
                { // nếu mà food trong booking food bằng với food id trong db thì trừ đi 
                    var bookingfood = bookingsFood.FirstOrDefault(x => x.FoodId == food.Id);
                    if (bookingfood is not null) food.Quanlity -= bookingfood.Quanlity;
                }
            }
            var result = await gateway.VerifyPaymentAsync(parameters, cancellationToken);
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
            await seatHoldService.ReleaseSeatsAsync(booking.ShowtimeId, showtimeseat.Select(x => x.SeatId).ToList());
            await unitOfwork.SaveChangeAsync(cancellationToken);
            return true;
        }

        public async Task<PaymentReturnDto> HandlerPaymentReturn(SortedDictionary<string, string> parameters, CancellationToken cancellationToken)
        {
            if (!parameters.TryGetValue(
          "vnp_TxnRef",
            out var bookingCode))
            {
                return new PaymentReturnDto(false, null, null);
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
            if (result.success)
            {
                return new PaymentReturnDto(true, bookingCode, payment.PaymentMethod);
            }
            return new PaymentReturnDto(false, bookingCode, payment.PaymentMethod);
        }
    }
}
