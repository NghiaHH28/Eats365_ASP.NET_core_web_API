using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EATS365_Library.DTO
{
    public class LoginDTO
    {
        [Required]
        [MaxLength(100)]
        [RegularExpression("^\\w+([.-]?\\w+)*@\\w+([.-]?\\w+)*(\\.\\w{2,3})+$")]
        public string AccountEmail { get; set; }
        [Required]
        [MaxLength(100)]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[^\\w\\s]).{6,}$")]
        public string AccountPassword { get; set; }
    }
}
