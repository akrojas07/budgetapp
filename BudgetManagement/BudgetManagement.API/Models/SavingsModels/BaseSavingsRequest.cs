using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagement.API.Models.SavingsModels
{
    public class BaseSavingsRequest
    {
        [Required(ErrorMessage ="Savings ID required")]
        public long? SavingsId { get; set; }
    }
}
