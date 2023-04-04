using EATS365_Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EATS365_Library.DTO
{
    public class CartDTO
    {
        public int CartId { get; set; }
        public int CartQuantity { get; set; }
        public string ProductId { get; set; }
        public string AccountId { get; set; }

        public virtual AccountDTO Account { get; set; }
        public virtual ProductDTO Product { get; set; }
    }
}
