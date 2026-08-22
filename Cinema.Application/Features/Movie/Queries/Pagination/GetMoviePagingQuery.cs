using MediatR;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Movie.Queries.Pagination
{
    public class GetMoviePagingQuery : ItemQueryParameters, IRequest<ApiResult<PagedResult<Domain.Enitities.Movie>>>
    {
        public string Keywords { get; set; }
    }
}
