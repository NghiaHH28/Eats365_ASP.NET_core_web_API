using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EATS365_Library.DTO
{
    public class APIResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
        public List<Object> List { get; set; }
    }
}
