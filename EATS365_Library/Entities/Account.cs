using System;
using System.Collections.Generic;

#nullable disable

namespace EATS365_Library.Entities
{
    public partial class Account
    {
        public Account()
        {
            Carts = new HashSet<Cart>();
            Orders = new HashSet<Order>();
            Reviews = new HashSet<Review>();
            Schedules = new HashSet<Schedule>();
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

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
