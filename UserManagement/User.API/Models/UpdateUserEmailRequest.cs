using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace User.API.Models
{
    public class UpdateUserEmailRequest:BaseAccountRequest
    {
        [Required(AllowEmptyStrings =false, ErrorMessage ="Email Address Required")]
        [EmailAddress(ErrorMessage ="Invalid email")]
        public string Email { get; set; }
    }
}
