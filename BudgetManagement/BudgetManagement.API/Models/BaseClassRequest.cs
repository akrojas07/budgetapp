using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagement.API.Models
{
    public class BaseClassRequest
    {
        [Required(ErrorMessage ="User ID is required")]
        public long UserId { get; set; }
    }
}
