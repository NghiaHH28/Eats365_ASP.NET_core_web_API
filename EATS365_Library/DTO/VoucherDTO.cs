using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EATS365_Library.DTO
{
    public class VoucherDTO
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
