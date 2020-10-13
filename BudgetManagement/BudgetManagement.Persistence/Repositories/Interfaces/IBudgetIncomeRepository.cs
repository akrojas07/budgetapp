using BudgetManagement.Persistence.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManagement.Persistence.Repositories.Interfaces
{
    public interface IBudgetIncomeRepository
    {
        Task<List<BudgetIncome>> GetAllIncomeByUserId(long userId);
        Task UpdateIncome(long incomeId, decimal incomeAmount);
        Task AddNewIncome(BudgetIncome budgetIncome);
        Task RemoveIncome(long incomeId);
        Task<BudgetIncome> GetIncomeByIncomeId(long incomeId);

    }
}
