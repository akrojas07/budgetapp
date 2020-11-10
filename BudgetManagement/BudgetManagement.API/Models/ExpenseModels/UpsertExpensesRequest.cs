using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagement.API.Models.ExpenseModels
{
    public class UpsertExpensesRequest
    {
        public List<Expense> Expenses { get; set; }
    }

    public class Expense
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string ExpenseType { get; set; }
        public decimal Amount { get; set; }
    }
}
