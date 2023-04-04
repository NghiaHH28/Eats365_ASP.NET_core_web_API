using System;
using System.Collections.Generic;

#nullable disable

namespace EATS365_Library.Entities
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public string OrderId { get; set; }
        public string OrderNote { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? OrderAcceptDate { get; set; }
        public DateTime? OrderDeliveryDate { get; set; }
        public string AccountId { get; set; }
        public string BuyerFullName { get; set; }
        public string BuyerPhone { get; set; }
        public string BuyerAddress { get; set; }
        public string VoucherId { get; set; }
        public int? ProductSalePercent { get; set; }
        public string AccIdofChef { get; set; }
        public string AccIdofShipper { get; set; }

        public virtual Account Account { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
