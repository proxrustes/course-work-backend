using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.Models
{
    public class User
    {
        [Key]
        [Required(ErrorMessage = "requaired")]
        public int userId { get; set; }
        [Required(ErrorMessage = "requaired")]
        public string userName { get; set; }
        [Required(ErrorMessage = "requaired")]
        public string role { get; set; }
        [Required(ErrorMessage = "requaired")]
        public string bio { get; set; }
        [Required(ErrorMessage = "requaired")]
        public string password { get; set; }
        public string? imageLink { get; set; }

        public ICollection<Character>? characters { get; set; }
    }
   
}
