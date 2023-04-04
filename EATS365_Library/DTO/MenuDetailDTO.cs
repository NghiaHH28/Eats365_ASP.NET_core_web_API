using EATS365_Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EATS365_Library.DTO
{
    public class MenuDetailDTO
    {
        public int MenuDid { get; set; }
        public int MenuId { get; set; }
        public string ProductId { get; set; }
        public int OriginalQuantity { get; set; }
        public int RemainingQuantity { get; set; }

        public virtual MenuDTO Menu { get; set; }
        public virtual ProductDTO Product { get; set; }
    }
}
