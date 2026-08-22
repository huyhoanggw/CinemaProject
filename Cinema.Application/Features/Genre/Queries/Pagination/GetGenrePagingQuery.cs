using MediatR;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Genre.Queries.Pagination
{
    public class GetGenrePagingQuery : ItemQueryParameters, IRequest<ApiResult<PagedResult<Domain.Enitities.Genre>>>
    {
       
    }
}
