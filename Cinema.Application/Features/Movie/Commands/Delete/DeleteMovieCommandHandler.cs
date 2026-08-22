using AutoMapper;
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

namespace Cinema.Application.Features.Movie.Commands.Delete
{
    public class DeleteMovieCommandHandler(IMovieRepository repository , ILogger<DeleteMovieCommandHandler> logger , IMapper mapper , IUnitOfWork unitOfWork) : IRequestHandler<DeleteMovieCommand, ApiResult<bool>>
    {
        public async Task<ApiResult<bool>> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("begin : DeleteMovieCommandHandler");
            var movieId = await repository.FindByIdAsync(request.Id);
            if (movieId is null) return new ApiErrorResult<bool>("Movie Id Not found");
            await repository.DeleteAsync(movieId.Id);
             await  unitOfWork.SaveChangeAsync(cancellationToken);
            logger.LogInformation(" end: DeleteMovieCommandHandler");
            return new ApiSuccessResult<bool>(true , "Remove Movie Successfully");
        }
    }
}
