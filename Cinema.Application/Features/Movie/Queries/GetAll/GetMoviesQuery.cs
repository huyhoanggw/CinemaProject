using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Movie.Queries.GetAll
{
    public class GetMoviesQuery : IRequest<List<Domain.Enitities.Movie>>
    {
    }
}
