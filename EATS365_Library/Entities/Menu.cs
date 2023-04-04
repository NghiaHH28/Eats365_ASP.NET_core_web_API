using System;
using System.Collections.Generic;

#nullable disable

namespace EATS365_Library.Entities
{
    public partial class Menu
    {
        public Menu()
        {
            MenuDetails = new HashSet<MenuDetail>();
            Schedules = new HashSet<Schedule>();
        }

        public int MenuId { get; set; }
        public DateTime Dates { get; set; }

        public virtual ICollection<MenuDetail> MenuDetails { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
