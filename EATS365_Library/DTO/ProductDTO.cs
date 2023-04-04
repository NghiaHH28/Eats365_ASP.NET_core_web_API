using EATS365_Library.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage = "Tên sản phẩm không được bỏ trống!")]
        [MaxLength(100, ErrorMessage = "Tên sản phẩm phải dưới 100 ký tự")]
        [RegularExpression("^[\\p{L} \\.'\\-]{2,}( [\\p{L} \\.'\\-]+)+$", ErrorMessage = "Tên sản phẩm phải có từ 2 từ trở lên!")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Mô tả sản phẩm không được bỏ trống!")]
        [MaxLength(500, ErrorMessage = "Mô tả sản phẩm phải dưới 500 từ!")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "Giá sản phẩm không được bỏ trống!")]
        [Range(0, 1000000, ErrorMessage = "Giá sản phẩm phải từ 0VND đến 1.000.000VND!")]
        public int ProductPrice { get; set; }

        [Required(ErrorMessage = "Phẩn trăm giảm giá không được bỏ trống!")]
        [Range(0, 100, ErrorMessage = "Phần trăm giảm giá phải từ 0 đến 100!")]
        public int ProductSalePercent { get; set; }

        public string ProductStatus { get; set; }

        [Required(ErrorMessage = "Hình ảnh không được bỏ trống!")]
        public string ProductImage { get; set; }

        public string CategoryId { get; set; }

        public virtual CategoryDTO Category { get; set; }
        public virtual ICollection<CartDTO> Carts { get; set; }
        public virtual ICollection<MenuDetailDTO> MenuDetails { get; set; }
        public virtual ICollection<OrderDetailDTO> OrderDetails { get; set; }
        public virtual ICollection<ReviewDTO> Reviews { get; set; }
    }
}
