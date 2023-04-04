using System;
using System.Collections.Generic;

#nullable disable

namespace EATS365_Library.Entities
{
    public partial class Schedule
    {
        public int ScheduleId { get; set; }
        public int MenuId { get; set; }
        public string AccountId { get; set; }
        public string ShiftId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Menu Menu { get; set; }
        public virtual Shift Shift { get; set; }
    }
}
