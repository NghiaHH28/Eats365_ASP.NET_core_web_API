using EATS365_Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EATS365_Library.DTO
{
    public class MenuDTO
    {
        public MenuDTO()
        {
            MenuDetails = new HashSet<MenuDetailDTO>();
            Schedules = new HashSet<ScheduleDTO>();
        }

        public int MenuId { get; set; }
        public DateTime Dates { get; set; }

        public virtual ICollection<MenuDetailDTO> MenuDetails { get; set; }
        public virtual ICollection<ScheduleDTO> Schedules { get; set; }
    }
}
