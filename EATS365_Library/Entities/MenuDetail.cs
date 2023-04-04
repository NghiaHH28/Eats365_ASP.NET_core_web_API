using System;
using System.Collections.Generic;

#nullable disable

namespace EATS365_Library.Entities
{
    public partial class MenuDetail
    {
        public int MenuDid { get; set; }
        public int MenuId { get; set; }
        public string ProductId { get; set; }
        public int OriginalQuantity { get; set; }
        public int RemainingQuantity { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual Product Product { get; set; }
    }
}
