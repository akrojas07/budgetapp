using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace User.API.Models
{
    public class BaseAccountRequest
    {
        [Required(ErrorMessage ="User ID is required")]
        public long UserId { get; set; }
    }
}
