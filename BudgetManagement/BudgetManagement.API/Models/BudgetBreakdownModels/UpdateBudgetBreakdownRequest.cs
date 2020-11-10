using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagement.API.Models.BudgetBreakdownModels
{
    public class UpdateBudgetBreakdownRequest: BaseBudgetBreakdownRequest
    {
        public long Id { get; set; }
    }
}
