using System;
using System.Collections.Generic;

#nullable disable

namespace EATS365_Library.Entities
{
    public partial class Voucher
    {
        public string VoucherId { get; set; }
        public string VoucherDescription { get; set; }
        public string VoucherStatus { get; set; }
        public int Discount { get; set; }
        public int VoucherQuantity { get; set; }
        public DateTime VoucherStartDay { get; set; }
        public DateTime VoucherEndday { get; set; }
    }
}
