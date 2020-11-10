using System;

namespace BudgetManagement.Persistence.Repositories.Entities
{
    public class BudgetExpenses
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string ExpenseType { get; set; }
        public decimal ExpenseAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
