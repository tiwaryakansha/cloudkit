using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLibrary.Models
{
    public class UserInputDetails
    {
        [Required]
        [StringLength(20, ErrorMessage = "Your First Name can contain only 20 characters")]
       // [Display(Name = "First Name")]
        public String Firstname { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage ="Your Last Name can contain only 20 characters")]
       // [Display(Name = "Last Name")]
        public String Lastname { get; set; }

        [Required(ErrorMessage = "Please enter Email Address")]
        public String Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Required]
        [Phone]
        public String ContactNo { get; set; }

        [Required]
        public String Address { get; set; }

        [Required]
        public String Role { get; set; }
    }
}
