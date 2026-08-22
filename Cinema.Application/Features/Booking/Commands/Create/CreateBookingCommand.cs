using Cinema.Domain.Enitities;
using MediatR;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Booking;
using SeedWorks.Models.Food;
using SeedWorks.Models.Seat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Booking.Commands.Create
{
    public class CreateBookingCommand : IRequest<ApiResult<CreateBookingModel>>
    {
        public Guid ShowtimeId { get; set; }

        public ICollection<CreateBookingSeatModel> BookingSeats { get; set; } = [];
        public ICollection<CreateBookingFoodModel> BookingFoods { get; set; } = [];

           }
}
