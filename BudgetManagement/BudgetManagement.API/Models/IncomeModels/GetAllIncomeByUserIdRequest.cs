using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagement.API.Models.IncomeModels
{
    public class GetAllIncomeByUserIdRequest: BaseClassRequest
    {
        [Required(ErrorMessage ="Income ID Required")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Income amount required")]
        public decimal IncomeAmount { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage ="Income Type required")]
        public string IncomeType { get; set; }
    }
}
