using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.API.Models
{
    public class CreateNewUserAccountRequest
    {
        [Required(AllowEmptyStrings =false, ErrorMessage ="Email Required")]
        [EmailAddress(ErrorMessage ="Invalid email")]
        public string Email { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage ="First Name Required")]
        public string FirstName { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage ="Last Name Required")]
        public string LastName { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage ="Password Required")]
        [MinLength(8, ErrorMessage ="Password must be at least 8 characters long")]
        public string Password { get; set; }

    }
}
