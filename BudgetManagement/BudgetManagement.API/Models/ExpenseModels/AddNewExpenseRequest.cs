using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagement.API.Models.ExpenseModels
{
    public class AddNewExpenseRequest: BaseClassRequest
    {

        [Required(AllowEmptyStrings = false, ErrorMessage ="Expense Type required")]
        public string ExpenseType { get; set; }

        [Required(ErrorMessage ="Expense Amount required")]
        public decimal? ExpenseAmount { get; set; }
    }
}
