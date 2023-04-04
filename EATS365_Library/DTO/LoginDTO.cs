using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EATS365_Library.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email không được bỏ trống!")]
        [MaxLength(100, ErrorMessage = "Email phải dưới 100 ký tự!")]
        [RegularExpression("^\\w+([.-]?\\w+)*@\\w+([.-]?\\w+)*(\\.\\w{2,3})+$", ErrorMessage = "Email không đúng định dạng!")]
        public string AccountEmail { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được bỏ trống!")]
        [MaxLength(100, ErrorMessage = "Mật khẩu phải dưới 100 ký tự!")]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[^\\w\\s]).{6,}$", ErrorMessage = "Email phải trên 6 " +
            "ký tự bao gồm ít nhất 1 chữ hoa, 1 chữ thường, 1 ký tự đặc biệt và số!")]
        public string AccountPassword { get; set; }
    }
}
