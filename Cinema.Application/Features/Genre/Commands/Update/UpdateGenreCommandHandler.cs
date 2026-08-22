using AutoMapper;
using Cinema.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Genre;
using SeedWorks.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Genre.Commands.Update
{
    public class UpdateGenreCommandHandler(IGenreRepository repository , ILogger<UpdateGenreCommandHandler> logger , IMapper mapper , IUnitOfWork unitOfWork) : IRequestHandler<UpdateGenreCommand, ApiResult<UpdateGenreModel>>
    {
        public async Task<ApiResult<UpdateGenreModel>> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin : UpdateGenreCommandHandler");
            var genreId = await repository.FindByIdAsync(request.Id);
            if (genreId is null) return new ApiErrorResult<UpdateGenreModel>("Genre Id not found");
            genreId.Name = request.Name;
            genreId.UpdateAt = DateTime.UtcNow;
            var result = await unitOfWork.SaveChangeAsync(cancellationToken);
            if (result <= 0) return new ApiErrorResult<UpdateGenreModel>("Error occurred while updating ");
            logger.LogInformation("end : UpdateGenreCommandHandler");
            return new ApiSuccessResult<UpdateGenreModel>("Update genre successfully");
        }
    }
}
