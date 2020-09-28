using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace User.API.Models
{
    public class UpdateAccountRequest:BaseAccountRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name required")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Last Name required")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Email required")]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string Email { get; set; }
    }
}
