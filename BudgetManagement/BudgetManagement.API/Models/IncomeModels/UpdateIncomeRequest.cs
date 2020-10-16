using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagement.API.Models.IncomeModels
{
    public class UpdateIncomeRequest:BaseIncomeRequest
    {
        [Required(ErrorMessage ="Income Amount Required")]
        public decimal IncomeAmount { get; set; }
    }
}
