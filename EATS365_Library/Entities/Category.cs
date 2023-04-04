using System;
using System.Collections.Generic;

#nullable disable

namespace EATS365_Library.Entities
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public string CategoryId { get; set; }
        public string CategoryDescription { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
