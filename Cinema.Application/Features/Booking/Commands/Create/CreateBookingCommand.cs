using Cinema.Contracts.Models.Booking;
using Cinema.Contracts.Models.Food;
using Cinema.Contracts.Models.Seat;
using Cinema.Contracts.Reponse;
using Cinema.Domain.Enitities;
using MediatR;
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
