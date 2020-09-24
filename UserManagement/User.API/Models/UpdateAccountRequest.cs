using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace User.API.Models
{
    public class UpdateAccountRequest:BaseAccountRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name Type required")]
        public string NameType { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Name required")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage ="Password required")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Email required")]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string Email { get; set; }
    }
}
