using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BudgetManagement.Domain.Models;

namespace BudgetManagement.Domain.Services.Interfaces
{
    public interface IBudgetSavingsServices
    {
        Task<List<BudgetSavingsModel>> GetAllSavingsByUserId(long userId);

        Task AddNewSaving(BudgetSavingsModel budgetSavingsModel);

        Task UpdateSaving(long savingId, decimal savingsAmount);
        Task RemoveSaving(long savingId);
    }
}
