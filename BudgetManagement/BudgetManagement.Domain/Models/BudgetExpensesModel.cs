using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetManagement.Domain.Models
{
    public class BudgetExpensesModel
    {
        public BudgetExpensesModel() { }

        public BudgetExpensesModel(long userId, string expenseType, decimal expenseAmount) 
        {
            UserId = userId;
            ExpenseType = expenseType;
            ExpenseAmount = expenseAmount;
        }
        public long Id { get; set; }
        public long UserId { get; set; }
        public string ExpenseType { get; set; }
        public decimal ExpenseAmount { get; set; }

/*        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }*/
    }
}
