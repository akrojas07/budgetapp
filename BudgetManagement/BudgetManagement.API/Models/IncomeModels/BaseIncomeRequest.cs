using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagement.API.Models.IncomeModels
{
    public class BaseIncomeRequest
    {
        [Required(ErrorMessage ="Income ID required")]
        public long? IncomeId { get; set; }
    }
}
