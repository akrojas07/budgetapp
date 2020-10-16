using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagement.API.Models.ExpenseModels
{
    public class BaseExpenseRequest
    {
        [Required(ErrorMessage ="Expense ID required")]
        public long? ExpenseId { get; set; }
    }
}
