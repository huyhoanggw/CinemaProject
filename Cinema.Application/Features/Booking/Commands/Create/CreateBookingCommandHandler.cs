using AutoMapper;
using Cinema.Application.Interfaces;
using Cinema.Domain.Enitities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Booking;
using SeedWorks.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Booking.Commands.Create
{
    public class CreateBookingCommandHandler(IBookingRepository repository ,IShowtimeRepository showtimeRepository,IShowtimeSeatRepository showtimeSeatRepository
        ,ILogger<CreateBookingCommandHandler> logger , IMapper mapper , IUnitOfWork unitOfWork , IHttpContextAccessor httpcontext , IFoodRepository FoodRepository
        , IBookingSeatRepository bookingSeatRepository , IBookingFoodRepository BookingFoodRepository) : IRequestHandler<CreateBookingCommand, ApiResult<CreateBookingModel>>
    {
        public async Task<ApiResult<CreateBookingModel>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin:CreateBookingCommandHandler ");
            // Get user
            // Get showtime
            // Get showtime seats
            // Validate seats
            // Create booking
            // Add booking seats + Hold seats
            // Get foods
            // Add booking foods + calculate price
            // Create booking
            // SaveChanges
            var userId = httpcontext.HttpContext.User?.FindFirst("uid")?.Value ?? httpcontext.HttpContext.User?.FindFirst("sub")?.Value
             ?? httpcontext.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException();
            }
            var showtime = await showtimeRepository.FindByIdAsync(request.ShowtimeId);
            if (showtime is null)
            {
                return new ApiErrorResult<CreateBookingModel>(
                    "Showtime not found");
            }
            var showTimeSeatIds =await showtimeSeatRepository.GetByShowtimeAndSeatIdsAsync(request.ShowtimeId,request.BookingSeats.Select(x => x.SeatId).ToList());
     
          
           
            if (showTimeSeatIds.Count != request.BookingSeats.Select(x => x.SeatId).Distinct().Count())
            {
                return new ApiErrorResult<CreateBookingModel>("Seat not found");
            }

            var booking = new Domain.Enitities.Booking()
            {
                Id = Guid.NewGuid(),
                UserId = userId!,
                BookingCode = $"BK{DateTime.UtcNow:yyMMdd}-{Guid.NewGuid().ToString("N").Substring(0, 6)}".ToUpper(),
                Showtime = showtime,
                ShowtimeId = request.ShowtimeId,
                CreateAt = DateTime.UtcNow,
                Status = BookingStatus.Pending,
                ExpiredAt = DateTime.UtcNow.AddMinutes(10)
            };
            if(showTimeSeatIds.Any(x => x.Status != ShowtimeSeatStatus.Available))
            {
                return new ApiErrorResult<CreateBookingModel>("One or more seats are not available");
            }
            foreach( var showtimeseat in showTimeSeatIds)
            {
                showtimeseat.Status = ShowtimeSeatStatus.Hold;
                showtimeseat.ReservedBy = userId;
                showtimeseat.ReservedAt = DateTime.UtcNow;
                showtimeseat.ReservedUntil = DateTime.UtcNow.AddMinutes(10);
                showtimeseat.UpdateAt = DateTime.UtcNow;
                booking.BookingSeats.Add(new BookingSeat()
                {
                    BookingId = booking.Id,
                    ShowtimeSeatId = showtimeseat.Id,
                    Price = showtimeseat.Price
                    
                });
                booking.TotalPrice += showtimeseat.Price;
            }
            var Foods = await FoodRepository.getFoodByIds(request.BookingFoods.Select(x => x.FoodId).ToList());
                foreach (var food in Foods)
            {
                var requestFood = request.BookingFoods.First(x => x.FoodId == food.Id);
                booking.BookingFoods.Add(new BookingFood()
                {
                    BookingId = booking.Id,
                    FoodId = food.Id,
                    UnitPrice = food.Price,
                    Quanlity = requestFood.Quanlity              
                    });
                booking.TotalPrice += food.Price * requestFood.Quanlity;
            }
               
            try
            {
                await unitOfWork.BeginTransaction(cancellationToken);
                await bookingSeatRepository.AddRange(booking.BookingSeats);
                await BookingFoodRepository.AddRange(booking.BookingFoods);
                await repository.CreateAsync(booking);
                var result = await unitOfWork.SaveChangeAsync(cancellationToken);
                if(result < 0)
                {
                    await unitOfWork.RollbackTransaction(cancellationToken);
                    return new ApiErrorResult<CreateBookingModel>("Error occurred while create booking");
                }
                await unitOfWork.CommitTransaction(cancellationToken);
                var bookingFromMapper = mapper.Map<CreateBookingModel>(booking);
                logger.LogInformation("end:CreateBookingCommandHandler ");
                return new ApiSuccessResult<CreateBookingModel>("Create Booking Successfully");

            }
            catch (DbUpdateConcurrencyException)
            {
                await unitOfWork.RollbackTransaction(cancellationToken);
                logger.LogInformation("end:CreateBookingCommandHandler ");
                return new ApiErrorResult<CreateBookingModel>("One or more seats were just reserved by another user");
            }
            catch
            {
                await unitOfWork.RollbackTransaction(cancellationToken);
                logger.LogInformation("end:CreateBookingCommandHandler ");
                throw;
            }



        }
    }
}

