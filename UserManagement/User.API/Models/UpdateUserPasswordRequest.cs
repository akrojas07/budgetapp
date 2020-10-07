using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace User.API.Models
{
    public class UpdateUserPasswordRequest:BaseAccountRequest
    {
        [Required(AllowEmptyStrings =false, ErrorMessage ="Password required")]
        [MinLength(8, ErrorMessage ="Password must be at least 8 characters")]
        [MaxLength(32, ErrorMessage ="Password cannot exceed 32 characters")]
        public string Password { get; set; }
    }
}
