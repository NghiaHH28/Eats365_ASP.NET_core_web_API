using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EATS365_Library.Paging
{
    public class PagingReponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Object List { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
    }
}
