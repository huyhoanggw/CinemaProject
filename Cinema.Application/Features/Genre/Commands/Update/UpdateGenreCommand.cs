using Cinema.Contracts.Models.Genre;
using Cinema.Contracts.Reponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Genre.Commands.Update
{
    public class UpdateGenreCommand : IRequest<ApiResult<UpdateGenreModel>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
