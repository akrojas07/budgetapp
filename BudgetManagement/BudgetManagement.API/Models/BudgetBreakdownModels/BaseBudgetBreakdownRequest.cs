using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagement.API.Models.BudgetBreakdownModels
{
    public class BaseBudgetBreakdownRequest:BaseClassRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Budget Type required")]
        public string BudgetType { get; set; }

        [Required(ErrorMessage ="Expense Breakdown Required")]
        public decimal ExpensesBreakdown { get; set; }

        [Required(ErrorMessage ="Savings Breakdown Required")]
        public decimal SavingsBreakdown { get; set; }
    }
}
