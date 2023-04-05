using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EATS365_Library.Paging
{
    public class Paging
    {
        public string SearchTitle { get; set; }
        public string SearchOrder { get; set; }
        public string SearchName { get; set; }
        public string SearchCategory { get; set; }
        public int? PageIndex { get; set; }
        public int PageCount { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

    }
}
