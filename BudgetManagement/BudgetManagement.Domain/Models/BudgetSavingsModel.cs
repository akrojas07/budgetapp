using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetManagement.Domain.Models
{
    public class BudgetSavingsModel
    {
        public BudgetSavingsModel() { }
        public BudgetSavingsModel(long userId, string savingsType, decimal savingsAmount) 
        {
            UserId = userId;
            SavingsType = savingsType;
            SavingsAmount = savingsAmount;
        
        }
        public long Id { get; set; }
        public long UserId { get; set; }
        public string SavingsType { get; set; }
        public decimal SavingsAmount { get; set; }
    }
}
