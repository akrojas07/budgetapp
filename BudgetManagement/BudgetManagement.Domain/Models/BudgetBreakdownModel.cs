using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetManagement.Domain.Models
{
    public class BudgetBreakdownModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string BudgetType { get; set; }
        public decimal ExpensesBreakdown { get; set; }
        public decimal SavingsBreakdown { get; set; }
    }
}
