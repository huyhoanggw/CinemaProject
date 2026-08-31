using AutoMapper;
using Cinema.Application.Interfaces;
using Cinema.Domain.Enitities;
using MediatR;
using Microsoft.Extensions.Logging;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Showtime;
using SeedWorks.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Showtime.Commands.Create
{
    public class CreateShowtimeCommandHandler(IShowtimeRepository showtimeRepository , ITheaterRepository theaterRepository,
        IShowtimeSeatRepository showtimeSeatRepository , IBookingRepository bookingRepository,IMovieRepository movieRepository, IMapper mapper ,
        IUnitOfWork unitOfWork, ILogger<CreateShowtimeCommandHandler> logger) : IRequestHandler<CreateShowtimeCommand, ApiResult<CreateShowtimeModel>>
    {
        public async Task<ApiResult<CreateShowtimeModel>> Handle(CreateShowtimeCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin : CreateShowtimeCommandHandler");
            var movie =await  movieRepository.FindByIdAsync(request.MovieId);
            
            if (movie is null) return new ApiErrorResult<CreateShowtimeModel>("Movie Id not found");

            var theater = await theaterRepository.FindByIdWithSeatsAsync(request.TheaterId);
            if(theater is null ) return new ApiErrorResult<CreateShowtimeModel>("Theater Id not found");
             var showtime = new Cinema.Domain.Enitities.Showtime()
            {
                Id = Guid.NewGuid(),
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                MovieId = request.MovieId,
                TheaterId = request.TheaterId,
                Movie = movie,
                BasePrice = request.BasePrice,
                CreateAt = DateTime.UtcNow,
                Status = Domain.Enitities.ShowtimeStatus.Open

            };
            var showtimeseats = new List<ShowtimeSeat>();
            foreach(var item in theater.Seats)
            {
                showtimeseats.Add(new ShowtimeSeat() {
                    Id = Guid.NewGuid(),
                    ShowtimeId = showtime.Id,
                    SeatId = item.Id,
                    Status = ShowtimeSeatStatus.Available,
                    Price = showtime.BasePrice
                });

            }
            showtime.ShowtimeSeats = showtimeseats;
            await showtimeRepository.CreateAsync(showtime);
            await showtimeSeatRepository.CreateRangeAsync(showtimeseats);
            var result = await unitOfWork.SaveChangeAsync();
            var showtimeTomapper = mapper.Map<CreateShowtimeModel>(showtime);
            logger.LogInformation("end : CreateShowtimeCommandHandler");
            return result > 0 ? new ApiSuccessResult<CreateShowtimeModel>(showtimeTomapper,"Create Showtime Successfully") : new ApiErrorResult<CreateShowtimeModel>("Error occured while create showtime");
        }
    }
}
