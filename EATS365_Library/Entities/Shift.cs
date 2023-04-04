using System;
using System.Collections.Generic;

#nullable disable

namespace EATS365_Library.Entities
{
    public partial class Shift
    {
        public Shift()
        {
            Schedules = new HashSet<Schedule>();
        }

        public string ShiftId { get; set; }
        public string ShiftDescription { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
