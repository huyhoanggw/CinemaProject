using MediatR;
using SeedWorks.ApiReponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Genre.Commands.Delete
{
    public class DeleteGenreCommand : IRequest<ApiResult<bool>>
    {
        public Guid Id { get; set; }
    }
}
