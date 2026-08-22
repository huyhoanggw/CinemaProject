using AutoMapper;
using Cinema.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Movie;
using SeedWorks.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Movie.Commands.Update
{
    public class UpdateMovieCommandHandler(IMovieRepository _repository , ILogger<UpdateMovieCommandHandler> _logger , IMapper _mapper , IUnitOfWork unitOfWork) : IRequestHandler<UpdateMovieCommand, ApiResult<UpdateMovieModel>>
    {
        public async Task<ApiResult<UpdateMovieModel>> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("begin:UpdateMovieCommandHandler");
            var update = await _repository.FindByIdAsync(request.Id);
            if (update is null) return new ApiErrorResult<UpdateMovieModel>("Movie Id not found");
                           update.Name = request.Name;
                update.MovieGenre = request.Genres;
                update.Description = request.Description;
                update.UpdateAt = DateTime.UtcNow;
                update.PosterUrl = request.PosterUrl;
                update.TrailerUrl = request.TrailerUrl;
               var result = await unitOfWork.SaveChangeAsync(cancellationToken);
                var updateMovie = _mapper.Map<UpdateMovieModel>(update);
            
            _logger.LogInformation("end:UpdateMovieCommandHandler");
                 if(result > 1 )
                return new ApiSuccessResult<UpdateMovieModel>(updateMovie, "Update Movie successfully");
       
            
            _logger.LogInformation("end:UpdateMovieCommandHandler");
            return new ApiErrorResult<UpdateMovieModel>("Error occurred while updating");

        }
    }
}
