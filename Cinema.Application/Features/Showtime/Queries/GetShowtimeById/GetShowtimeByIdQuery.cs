using MediatR;
using SeedWorks.ApiReponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Showtime.Queries.GetMovieById
{
    public class GetShowtimeByIdQuery : IRequest<ApiResult<Domain.Enitities.Showtime>>
    {
        public Guid Id { get; set; }
    }
}
