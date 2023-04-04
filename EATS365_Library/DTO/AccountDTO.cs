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
        [Required]
        [MaxLength(100)]
        [RegularExpression("^\\w+([.-]?\\w+)*@\\w+([.-]?\\w+)*(\\.\\w{2,3})+$")]
        public string AccountEmail { get; set; }
        [Required]
        [MaxLength(100)]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[^\\w\\s]).{6,}$")]
        public string AccountPassword { get; set; }
        public string AccountStatus { get; set; }
        [Required]
        [MaxLength(100)]
        [RegularExpression("^[\\p{L} \\.'\\-]{2,}( [\\p{L} \\.'\\-]+)+$")]
        public string AccountName { get; set; }
        [Required]
        [MaxLength(200)]
        public string AccountAddress { get; set; }
        [Required]
        [MaxLength(10)]
        [RegularExpression("^0\\d{9}$")]
        public string AccountPhone { get; set; }
        [Required]
        public DateTime AccountBirthDay { get; set; }
        public DateTime AccountStartDate { get; set; }
        public DateTime? AccountEndDate { get; set; }

        public virtual ICollection<CartDTO> Carts { get; set; }
        public virtual ICollection<OrderDTO> Orders { get; set; }
        public virtual ICollection<ReviewDTO> Reviews { get; set; }
        public virtual ICollection<ScheduleDTO> Schedules { get; set; }
    }
}
