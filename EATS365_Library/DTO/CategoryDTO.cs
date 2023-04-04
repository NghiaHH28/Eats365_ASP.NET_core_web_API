using EATS365_Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EATS365_Library.DTO
{
    public class CategoryDTO
    {
        public CategoryDTO()
        {
            Products = new HashSet<ProductDTO>();
        }

        public string CategoryId { get; set; }
        public string CategoryDescription { get; set; }

        public virtual ICollection<ProductDTO> Products { get; set; }
    }
}
