using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetManagement.Persistence.Repositories.Entities
{
    public class BudgetSavings
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string SavingsType { get; set; }
        public decimal SavingsAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
