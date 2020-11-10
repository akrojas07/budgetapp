using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetManagement.Persistence.Repositories.Entities
{
    public class BudgetIncome
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string IncomeType { get; set; }
        public decimal IncomeAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
