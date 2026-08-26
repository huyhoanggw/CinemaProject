using AutoMapper;
using Cinema.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Logging;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Genre;
using SeedWorks.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Genre.Commands.Create
{
    public class CreateGenreCommandHandler(IGenreRepository repository , ILogger<CreateGenreCommandHandler> logger , IMapper mapper , IUnitOfWork unitOfWork) : IRequestHandler<CreateGenreCommand, ApiResult<CreateGenreModel>>
    {
        public async Task<ApiResult<CreateGenreModel>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin :CreateGenreCommandHandler ");
            var genre = await repository.GetByAsync(_ => _.Name.Equals(request.Name));
            if (genre is not null) return new ApiErrorResult<CreateGenreModel>("Genre name duplicate");
            var genreToAdd = new Domain.Enitities.Genre()
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };
            await repository.CreateAsync(genreToAdd);
             var result = await unitOfWork.SaveChangeAsync();
            if (result <= 0) return new ApiErrorResult<CreateGenreModel>("Error occurred while create genre");
            var genreTomapper = mapper.Map<CreateGenreModel>(genre);

            logger.LogInformation("end:CreateGenreCommandHandler ");
            return new ApiSuccessResult<CreateGenreModel>(genreTomapper, "create genre successfully");
        }
    }
}
