using Cinema.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using SeedWorks.ApiReponse;
using SeedWorks.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Genre.Commands.Delete
{
    public class DeleteGenreCommandHandler(IGenreRepository repository , ILogger<DeleteGenreCommandHandler> logger , IUnitOfWork unitOfWork) : IRequestHandler<DeleteGenreCommand, ApiResult<bool>>
    {
        public async Task<ApiResult<bool>> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin : DeleteGenreCommandHandler");
            var genreId = await repository.FindByIdAsync(request.Id);
            if (genreId is null) return new ApiErrorResult<bool>("Genre Id Not found");
            await repository.DeleteAsync(genreId.Id);
            var result = await unitOfWork.SaveChangeAsync(cancellationToken);
            
            logger.LogInformation("begin : DeleteGenreCommandHandler");
            return result > 0 ? new ApiSuccessResult<bool>("Delete Genre successfully") : new ApiErrorResult<bool>("Error occurred while remove genre");
        }
    }
}
