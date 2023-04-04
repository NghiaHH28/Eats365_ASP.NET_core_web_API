using System;
using System.Collections.Generic;

#nullable disable

namespace EATS365_Library.Entities
{
    public partial class Review
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

        public virtual Account Account { get; set; }
        public virtual Product Product { get; set; }
    }
}
