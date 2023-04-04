using EATS365_Library.Entities;
using System;
using System.Collections.Generic;
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
        public string AccountEmail { get; set; }
        public string AccountPassword { get; set; }
        public string AccountStatus { get; set; }
        public string AccountName { get; set; }
        public string AccountAddress { get; set; }
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
