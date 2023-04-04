using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EATS365_Library.DTO
{
    public class VoucherDTO
    {
        public string VoucherId { get; set; }
        [Required]
        public string VoucherDescription { get; set; }
        public string VoucherStatus { get; set; }
        [Required]
        [Range(0, 100)]
        public int Discount { get; set; }
        [Required]
        [Range(0, 1000)] 
        public int VoucherQuantity { get; set; }
        public DateTime VoucherStartDay { get; set; }
        public DateTime VoucherEndday { get; set; }
    }
}
