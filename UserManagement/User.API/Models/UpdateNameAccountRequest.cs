using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace User.API.Models
{
    public class UpdateNameAccountRequest:BaseAccountRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name Type required")]
        public string NameType { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Name required")]
        public string Name { get; set; }
    }
}
