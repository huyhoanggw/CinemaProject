using MediatR;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Genre.Commands.Create
{
    public class CreateGenreCommand : IRequest<ApiResult<CreateGenreModel>>
    {
        public string Name { get; set; }
    }
}
