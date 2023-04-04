using EATS365_Library.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EATS365_Library.DTO
{
    public class AccountDTO
    {
        public AccountDTO()
        {
            Carts = new HashSet<CartDTO>();
            Orders = new HashSet<OrderDTO>();
            Reviews = new HashSet<ReviewDTO>();
            Schedules = new HashSet<ScheduleDTO>();
        }
        public string AccountId { get; set; }

        [Required(ErrorMessage = "Email không được bỏ trống!")]
        [MaxLength(100, ErrorMessage = "Email phải dưới 100 ký tự!")]
        [RegularExpression("^\\w+([.-]?\\w+)*@\\w+([.-]?\\w+)*(\\.\\w{2,3})+$", ErrorMessage = "Email không đúng định dạng!")]
        public string AccountEmail { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được bỏ trống!")]
        [MaxLength(100, ErrorMessage = "Mật khẩu phải dưới 100 ký tự!")]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[^\\w\\s]).{6,}$", ErrorMessage = "Email phải trên 6 " +
            "ký tự bao gồm ít nhất 1 chữ hoa, 1 chữ thường, 1 ký tự đặc biệt và số!")]
        public string AccountPassword { get; set; }

        public string AccountStatus { get; set; }

        [Required(ErrorMessage = "Tên không được bỏ trống!")]
        [MaxLength(100, ErrorMessage = "Tên phải dưới 100 ký tự!")]
        [RegularExpression("^[\\p{L} \\.'\\-]{2,}( [\\p{L} \\.'\\-]+)+$", ErrorMessage = "Tên phải có từ 2 từ trở lên!")]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được bỏ trống!")]
        [MaxLength(200, ErrorMessage = "Địa chỉ phải dưới 200 ký tự!")]
        public string AccountAddress { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được bỏ trống!")]
        [RegularExpression("^0\\d{9}$", ErrorMessage = "Số điện thoại không đúng định dạng!")]
        public string AccountPhone { get; set; }

        public DateTime AccountBirthDay { get; set; }
        public DateTime AccountStartDate { get; set; }
        public DateTime? AccountEndDate { get; set; }

        public virtual ICollection<CartDTO> Carts { get; set; }
        public virtual ICollection<OrderDTO> Orders { get; set; }
        public virtual ICollection<ReviewDTO> Reviews { get; set; }
        public virtual ICollection<ScheduleDTO> Schedules { get; set; }
    }
}
