using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class UserDetails
    {
        [Key]
        public Int32 Id { get; set; }
        [Required]
        [MaxLength(100)]
        public String Firstname { get; set; }
        [Required]
        [MaxLength(100)]
        public String Lastname { get; set; }
        [Required]
        [MaxLength(100)]
        public String Email { get; set; }
        [Required]
        [MaxLength(100)]
        [DataType(DataType.Password)]
        public String Password { get; set; }
        [Required]
        [MaxLength(100)]
        public String ContactNo { get; set; }
        [Required]
        [MaxLength(100)]
        public String Address { get; set; }
        [Required]
        [MaxLength(100)]
        public String Role { get; set; }
    }
}
