using EATS365_Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EATS365_Library.DTO
{
    public class ReviewDTO
    {
        public string ReviewId { get; set; }
        public int Rating { get; set; }
        public string Review1 { get; set; }
        public string ReviewStatus { get; set; }
        public DateTime ReviewDay { get; set; }
        public DateTime? ReviewRemoveDay { get; set; }
        public string ProductId { get; set; }
        public string AccountId { get; set; }
        public string ReplyId { get; set; }

        public virtual AccountDTO Account { get; set; }
        public virtual ProductDTO Product { get; set; }
    }
}
