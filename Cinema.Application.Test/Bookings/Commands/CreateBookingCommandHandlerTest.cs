using AutoMapper;
using Castle.Core.Logging;
using Cinema.Application.Features.Booking.Commands.Create;
using Cinema.Application.Interfaces;
using Cinema.Domain.Enitities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using SeedWorks.Models.Booking;
using SeedWorks.Models.Food;
using SeedWorks.Models.Seat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Test.Bookings.Commands
{
    public class CreateBookingCommandHandlerTest 
    {
        private readonly Mock<IBookingRepository> _bookingRepository;
        private readonly Mock<IShowtimeRepository> _showtimeRepository;
        private readonly Mock<IShowtimeSeatRepository> _showtimeSeatRepository;
        private readonly Mock<IBookingSeatRepository> _bookingSeatRepository;
        private readonly Mock<IBookingFoodRepository> _bookingFoodRepository;
        private readonly Mock<IFoodRepository> _foodRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ILogger<CreateBookingCommandHandler>> _logger;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IHttpContextAccessor> _httpcontext;
        private CreateBookingCommandHandler _handler;
        public CreateBookingCommandHandlerTest()
        {
            _bookingRepository = new Mock<IBookingRepository>();
            _showtimeRepository = new Mock<IShowtimeRepository>();
            _showtimeSeatRepository = new Mock<IShowtimeSeatRepository>();
            _bookingSeatRepository = new Mock<IBookingSeatRepository>();
            _bookingFoodRepository = new Mock<IBookingFoodRepository>();
            _foodRepository = new Mock<IFoodRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _logger = new Mock<ILogger<CreateBookingCommandHandler>>();
            _mapper = new Mock<IMapper>();
            _httpcontext = new Mock<IHttpContextAccessor>();
            _handler = new CreateBookingCommandHandler(_bookingRepository.Object , _showtimeRepository.Object,_showtimeSeatRepository.Object, _logger.Object
                , _mapper.Object,_unitOfWork.Object,_httpcontext.Object, _foodRepository.Object,_bookingSeatRepository.Object,_bookingFoodRepository.Object);
              
        }

      
        [Fact]
        public async Task Handler_ShouldreturnError_WhenShowtimeNotFound()
        {
            //Arrange
            var showtimeId = Guid.NewGuid();
            var seatId = Guid.NewGuid();
            var foodId = Guid.NewGuid();
            var bookingseats = new List<CreateBookingSeatModel> 
            {
               new(){ SeatId = seatId }
            };
            var bookingFoods = new List<CreateBookingFoodModel> 
            {
                 new(){ FoodId = foodId , Quanlity = 1  }
            };
            
            var command = new CreateBookingCommand()
            {
                ShowtimeId = showtimeId,
                BookingSeats = bookingseats,
                BookingFoods = bookingFoods
            };
            //ShowtimeSeat
            _showtimeSeatRepository.Setup(x => x.GetByShowtimeAndSeatIdsAsync(showtimeId, It.IsAny<List<Guid>>()))
                .ReturnsAsync(new List<ShowtimeSeat>() { new ShowtimeSeat() { Id = Guid.NewGuid(), ShowtimeId = showtimeId, SeatId = seatId } });
            // httpcontext
            var claims = new List<Claim>()
                {
                    new Claim("uid", "test-user")
                };
            var claimIdentity = new ClaimsIdentity(claims);
            var claimPrincipal = new ClaimsPrincipal(claimIdentity);
            var user = new DefaultHttpContext() { User = claimPrincipal };
            _httpcontext.Setup(x => x.HttpContext).Returns(user);
            //Showtime
            _showtimeRepository.Setup(x => x.FindByIdAsync(showtimeId)).ReturnsAsync((Showtime?)null);
            //food
            _foodRepository.Setup(x => x.FindByIdAsync(foodId)).ReturnsAsync(new Food() { Id = foodId , Quanlity = 50});
            //Act 
            var result = await _handler.Handle(command , CancellationToken.None);
            // Assert
            Assert.False(result.IsSuccess);
        }
        [Fact]
        public async Task Handler_ShouldreturnContinue_WhenShowtimeExists()
        {
            //Arrange
            var showtimeId = Guid.NewGuid();
            var seatId = Guid.NewGuid();
            var foodId = Guid.NewGuid();
            var bookingseats = new List<CreateBookingSeatModel>
            {
               new(){ SeatId = seatId }
            };
            var bookingFoods = new List<CreateBookingFoodModel>
            {
                 new(){ FoodId = foodId , Quanlity = 1  }
            };

            var command = new CreateBookingCommand()
            {
                ShowtimeId = showtimeId,
                BookingSeats = bookingseats,
                BookingFoods = bookingFoods
            };
            //ShowtimeSeat
            _showtimeSeatRepository.Setup(x => x.GetByShowtimeAndSeatIdsAsync(showtimeId, It.IsAny<List<Guid>>()))
                .ReturnsAsync(new List<ShowtimeSeat>() { new ShowtimeSeat() 
                {
                      Id = Guid.NewGuid(),
                      ShowtimeId = showtimeId,
                      SeatId = seatId,
                      Status = ShowtimeSeatStatus.Available,
                      Price = 100000

                } });
            // httpcontext
            var claims = new List<Claim>()
                {
                    new Claim("uid", "test-user")
                };
            var claimIdentity = new ClaimsIdentity(claims);
            var claimPrincipal = new ClaimsPrincipal(claimIdentity);
            var user = new DefaultHttpContext() { User = claimPrincipal };
            _httpcontext.Setup(x => x.HttpContext).Returns(user);
            //Showtime
            _showtimeRepository.Setup(x => x.FindByIdAsync(showtimeId)).ReturnsAsync(new Showtime() { Id = showtimeId});
            //food
            _foodRepository.Setup(x => x.getFoodByIds(It.IsAny<List<Guid>>())).ReturnsAsync(new List<Food>() {
                new Food() { Id = foodId,Quanlity = 50 }
             
            });

            // unitOfwork 
            _unitOfWork
               .Setup(x => x.BeginTransaction(
                 It.IsAny<CancellationToken>()))
                     .Returns(Task.CompletedTask);

            _unitOfWork
                .Setup(x => x.SaveChangeAsync(
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            _unitOfWork
                .Setup(x => x.CommitTransaction(
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            _unitOfWork
                .Setup(x => x.RollbackTransaction(
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
               // mapper 
            _mapper.Setup(x => x.Map<CreateBookingModel>(It.IsAny<Booking>)).Returns(new CreateBookingModel());
            //booking
            _bookingRepository.Setup(x => x.CreateAsync(It.IsAny<Booking>())).ReturnsAsync(new Booking() { Id = Guid.NewGuid() });
            _bookingSeatRepository.Setup(x => x.AddRange(It.IsAny<List<BookingSeat>>())).Returns(Task.CompletedTask);

            _bookingFoodRepository.Setup(x => x.AddRange(It.IsAny<List<BookingFood>>())).Returns(Task.CompletedTask);
            //Act 
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.True(result.IsSuccess);
        }
        [Fact]
        public async Task Handler_ShouldReturnError_WhenSeatNotFound()
        {
            //Arrange
            var showtimeId = Guid.NewGuid();
            var seatId = Guid.NewGuid();
            var foodId = Guid.NewGuid();
            var bookingseats = new List<CreateBookingSeatModel>
            {
               new(){ SeatId = seatId }
            };
            var bookingFoods = new List<CreateBookingFoodModel>
            {
                 new(){ FoodId = foodId , Quanlity = 1  }
            };

            var command = new CreateBookingCommand()
            {
                ShowtimeId = showtimeId,
                BookingSeats = bookingseats,
                BookingFoods = bookingFoods
            };
            //ShowtimeSeat
            _showtimeSeatRepository.Setup(x => x.GetByShowtimeAndSeatIdsAsync(showtimeId, It.IsAny<List<Guid>>()))
                .ReturnsAsync((List<ShowtimeSeat>?)null);
            // httpcontext
            var claims = new List<Claim>()
                {
                    new Claim("uid", "test-user")
                };
            var claimIdentity = new ClaimsIdentity(claims);
            var claimPrincipal = new ClaimsPrincipal(claimIdentity);
            var user = new DefaultHttpContext() { User = claimPrincipal };
            _httpcontext.Setup(x => x.HttpContext).Returns(user);
            //Showtime
            _showtimeRepository.Setup(x => x.FindByIdAsync(showtimeId)).ReturnsAsync((Showtime?)null);
            //food
            _foodRepository.Setup(x => x.FindByIdAsync(foodId)).ReturnsAsync(new Food() { Id = foodId, Quanlity = 50 });
            //Act 
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
