using MediatR;
using SeedWorks.ApiReponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Movie.Commands.Delete
{
    public class DeleteMovieCommand : IRequest<ApiResult<bool>>
    {
        public Guid Id { get; set; }
    }
}
