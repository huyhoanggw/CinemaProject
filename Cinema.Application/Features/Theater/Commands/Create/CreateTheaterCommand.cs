using MediatR;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Theater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Theater.Commands.Create
{
    public class CreateTheaterCommand : IRequest<ApiResult<CreateTheaterModel>>
    {
        public string Name { get; set; }
    }
}
