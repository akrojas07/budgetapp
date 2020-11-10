using BudgetManagement.Persistence.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManagement.Persistence.Repositories.Interfaces
{
    public interface IBudgetSavingsRepository
    {
        Task<List<BudgetSavings>> GetAllSavingsByUserId(long userId);

        Task AddNewSaving(BudgetSavings budgetSavings);
        Task UpdateSaving(long savingId, decimal savingAmount);
        Task RemoveSaving(long savingId);
        Task<BudgetSavings> GetSavingBySavingId(long savingId);
        Task UpsertSavings(List<BudgetSavings> budgetSavings);
    }
}
