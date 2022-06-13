using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Username requaired")]
        [MaxLength(24, ErrorMessage = "Max length is 24 symbols")]
        [Display(Name="Username")]
        public string username { get; set; }

        [Required]
        [Display(Name = "Your password")]
        public string password { get; set; }
    }
}
