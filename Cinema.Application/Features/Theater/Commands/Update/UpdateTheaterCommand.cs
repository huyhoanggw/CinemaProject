using MediatR;
using SeedWorks.ApiReponse;
using SeedWorks.Models.Theater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Theater.Commands.Update
{
    public class UpdateTheaterCommand : IRequest<ApiResult< UpdateTheaterModel>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
