using EATS365_Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EATS365_Library.DTO
{
    public class OrderDetailDTO
    {
        public string OrderDid { get; set; }
        public int OrderDquantity { get; set; }
        public int OrderDprice { get; set; }
        public string ProductId { get; set; }
        public string OrderId { get; set; }

        public virtual OrderDTO Order { get; set; }
        public virtual ProductDTO Product { get; set; }
    }
}
