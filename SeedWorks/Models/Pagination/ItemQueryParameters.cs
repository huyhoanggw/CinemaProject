using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedWorks.Models.Pagination
{
    public class ItemQueryParameters 
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; }
        private int _pageSize;
        //public string? Search {  get; set; }
        public int PageSize { get => _pageSize; set => _pageSize = value >= MaxPageSize ? MaxPageSize : value; }
    }
}
