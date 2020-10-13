using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetManagement.Domain.Models
{
    public class BudgetIncomeModel
    {
        public BudgetIncomeModel() { }

        public BudgetIncomeModel(long userId, string incomeType, decimal incomeAmount ) 
        {
            UserId = userId;
            IncomeType = incomeType;
            IncomeAmount = incomeAmount;
        
        }
        public long Id { get; set; }
        public long UserId { get; set; }
        public string IncomeType { get; set; }
        public decimal IncomeAmount { get; set; }
    }
}
