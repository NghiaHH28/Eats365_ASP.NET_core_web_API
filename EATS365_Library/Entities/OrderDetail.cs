using System;
using System.Collections.Generic;

#nullable disable

namespace EATS365_Library.Entities
{
    public partial class OrderDetail
    {
        public string OrderDid { get; set; }
        public int OrderDquantity { get; set; }
        public int OrderDprice { get; set; }
        public string ProductId { get; set; }
        public string OrderId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
