using EATS365_Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EATS365_Library.DTO
{
    public class ScheduleDTO
    {
        public int ScheduleId { get; set; }
        public int MenuId { get; set; }
        public string AccountId { get; set; }
        public string ShiftId { get; set; }

        public virtual AccountDTO Account { get; set; }
        public virtual MenuDTO Menu { get; set; }
        public virtual ShiftDTO Shift { get; set; }
    }
}
