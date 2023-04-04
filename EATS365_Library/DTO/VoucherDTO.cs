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
        [Required(ErrorMessage = "Mã voucher không được để trống!")]
        [MaxLength(50, ErrorMessage = "Max voucher phải dưới 50 ký tự!")]
        public string VoucherId { get; set; }
        public string VoucherDescription { get; set; }
        public string VoucherStatus { get; set; }
        [Required(ErrorMessage = "Discount của voucher không được để trống!")]
        [Range(0, 100, ErrorMessage = "Discount của voucher phải từ 0 đến 100!")]
        public int Discount { get; set; }
        [Required(ErrorMessage = "Số lượng voucher không được để trống!")]
        [Range(0, 1000, ErrorMessage = "Số lượng voucher phải từ 0 đến 1000!")] 
        public int VoucherQuantity { get; set; }
        public DateTime VoucherStartDay { get; set; }
        public DateTime VoucherEndday { get; set; }
    }
}
