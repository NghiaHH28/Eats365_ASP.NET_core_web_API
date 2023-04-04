using EATS365_Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EATS365_Library.DTO
{
    public class ProductDTO
    {
        public ProductDTO()
        {
            Carts = new HashSet<CartDTO>();
            MenuDetails = new HashSet<MenuDetailDTO>();
            OrderDetails = new HashSet<OrderDetailDTO>();
            Reviews = new HashSet<ReviewDTO>();
        }

        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int ProductPrice { get; set; }
        public int ProductSalePercent { get; set; }
        public string ProductStatus { get; set; }
        public string ProductImage { get; set; }
        public string CategoryId { get; set; }

        public virtual CategoryDTO Category { get; set; }
        public virtual ICollection<CartDTO> Carts { get; set; }
        public virtual ICollection<MenuDetailDTO> MenuDetails { get; set; }
        public virtual ICollection<OrderDetailDTO> OrderDetails { get; set; }
        public virtual ICollection<ReviewDTO> Reviews { get; set; }
    }
}
