using EATS365_Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EATS365_Library.DTO
{
    public class ShiftDTO
    {
        public ShiftDTO()
        {
            Schedules = new HashSet<ScheduleDTO>();
        }

        public string ShiftId { get; set; }
        public string ShiftDescription { get; set; }

        public virtual ICollection<ScheduleDTO> Schedules { get; set; }
    }
}
