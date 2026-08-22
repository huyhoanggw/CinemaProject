using Cinema.Domain.Enitities;
using MediatR;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Movie.Commands.Update
{
    public class UpdateMovieCommand : IRequest<ApiResult<UpdateMovieModel>>
    {
        public Guid Id;
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? PosterUrl { get; set; }
        public string? TrailerUrl { get; set; }
        public List<MovieGenre> Genres { get; set; } = [];
    }
}
