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

namespace Cinema.Application.Features.Movie.Commands.Create
{
    public class CreateMovieCommandHandler(IMovieRepository _repository , ILogger<CreateMovieCommandHandler> _logger , IMapper _mapper , IUnitOfWork unitOfWork) : IRequestHandler<CreateMovieCommand, ApiResult<CreateMovieDto>>
    {
          public async Task<ApiResult<CreateMovieDto>> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("begin : CreateMovieCommandHandler");
            var movie = await  _repository.GetByAsync(_ => _.Name.Equals(request.Name));
            if (movie is not null) return new ApiErrorResult<CreateMovieDto>("Movie Name Duplicate");
            try
            {
                var movieToAdd = new Domain.Enitities.Movie()
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    PosterUrl = request.PosterUrl,
                    TrailerUrl = request.TrailerUrl,
                    MovieGenre = request.Genres,
                    Description = request.Description
                    
                };
                await _repository.CreateAsync(movieToAdd);
                await unitOfWork.SaveChangeAsync(cancellationToken);
                var dto = _mapper.Map<CreateMovieDto>(movieToAdd);
            _logger.LogInformation("end : CreateMovieCommandHandler");
                return new ApiSuccessResult<CreateMovieDto>(dto,"create movie success");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            _logger.LogInformation("end : CreateMovieCommandHandler");
            return new ApiErrorResult<CreateMovieDto>("Error occurred while create movie");

        }
    }
}
