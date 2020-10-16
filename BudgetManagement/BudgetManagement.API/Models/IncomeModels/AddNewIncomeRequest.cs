using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagement.API.Models.IncomeModels
{
    public class AddNewIncomeRequest: BaseClassRequest
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "Income Type required")]
        public string IncomeType { get; set; }

        [Required(ErrorMessage = "Income Amount required")]
        public decimal? IncomeAmount { get; set; }
    }
}
