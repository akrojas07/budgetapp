using BudgetManagement.Persistence.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManagement.Persistence.Repositories.Interfaces
{
    public interface IBudgetBreakdownRepository
    {
        Task AddNewBudgetBreakdownByUserId(BudgetBreakdown budgetBreakdown);
        Task<string> GetBudgetTypeByUserId(long userId);
        Task<BudgetBreakdown> GetBudgetBreakdownByUserId(long userId);
        Task RemoveBudgetBreakdownByUserId(long userId);
        Task UpdateBudgetBreakdownByUserId(BudgetBreakdown budgetBreakdown);

    }
}
