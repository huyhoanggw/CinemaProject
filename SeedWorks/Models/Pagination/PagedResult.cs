using Cinema.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedWorks.Models.Pagination
{
    public class PagedResult<T> where T : class
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
       public int TotalCount { get; set; }
        public int TotalPage { get; set; }
        public List<T> Items { get; set; } = new List<T>();
    }
}
