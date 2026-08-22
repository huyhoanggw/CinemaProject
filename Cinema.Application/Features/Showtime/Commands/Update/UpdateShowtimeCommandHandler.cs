using AutoMapper;
using Cinema.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Showtime;
using SeedWorks.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Showtime.Commands.Update
{
    public class UpdateShowtimeCommandHandler(IShowtimeRepository showtimeRepository , ITheaterRepository theaterRepository , IMovieRepository movieRepository , IMapper mapper 
        , IUnitOfWork unitOfWork , ILogger<UpdateShowtimeCommandHandler> logger) : IRequestHandler<UpdateShowtimeCommand, ApiResult<UpdateShowtimeModel>>
    {
        public async Task<ApiResult<UpdateShowtimeModel>> Handle(UpdateShowtimeCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin:UpdateShowtimeCommandHandler");
           var showtime = await showtimeRepository.FindByIdAsync(request.ShowtimeId);
            if (showtime is null) return new ApiErrorResult<UpdateShowtimeModel>("Showtime Id not found");

            var theater = await theaterRepository.FindByIdAsync(request.Theater.Id);
            if (theater is null) return new ApiErrorResult<UpdateShowtimeModel>("Theater Id not found");

            var movie = await movieRepository.FindByIdAsync(request.Movie.Id);
            if (movie is null) return new ApiErrorResult<UpdateShowtimeModel>("Movie Id not found");
            showtime.Theater = theater;
            showtime.Movie = movie;
            showtime.StartTime = request.StartTime;
            showtime.EndTime = request.EndTime;
            showtime.Status = request.Status;
            var result = await unitOfWork.SaveChangeAsync(cancellationToken);
            logger.LogInformation("end:UpdateShowtimeCommandHandler");
            var showtimeToMapper = mapper.Map<UpdateShowtimeModel>(showtime);
            return result > 0 ? new ApiSuccessResult<UpdateShowtimeModel>(showtimeToMapper, "Update Showtime Successfully") : new ApiErrorResult<UpdateShowtimeModel>("Error occurred while update showtime");
        }
    }
}
