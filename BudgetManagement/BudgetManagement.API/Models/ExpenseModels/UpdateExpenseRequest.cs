using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagement.API.Models.ExpenseModels
{
    public class UpdateExpenseRequest:BaseExpenseRequest
    {

        [Required(ErrorMessage ="Expense Amount Required")]
        public decimal ExpenseAmount { get; set; }
    }
}
