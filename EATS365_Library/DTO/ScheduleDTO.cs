using EATS365_Library.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EATS365_Library.DTO
{
    public class ScheduleDTO
    {
        public int ScheduleId { get; set; }
        [Required]
        public int MenuId { get; set; }
        [Required]
        public string AccountId { get; set; }
        [Required]
        public string ShiftId { get; set; }

        public virtual AccountDTO Account { get; set; }
        public virtual MenuDTO Menu { get; set; }
        public virtual ShiftDTO Shift { get; set; }
    }
}
