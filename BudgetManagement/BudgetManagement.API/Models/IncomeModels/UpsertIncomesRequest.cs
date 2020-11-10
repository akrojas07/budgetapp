using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagement.API.Models.IncomeModels
{
    public class UpsertIncomesRequest
    {
        public List<Income> Incomes { get; set; }
    }

    public class Income
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string IncomeType { get; set; }
        public decimal Amount { get; set; }
    }
}
