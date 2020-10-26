using BudgetManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManagement.Domain.Services.Interfaces
{
    public interface IBudgetBreakdownServices
    {
        Task AddNewBudgetBreakdownByUserId(BudgetBreakdownModel budgetBreakdownModel);
        Task<string> GetBudgetTypeByUserId(long userId);
        Task<BudgetBreakdownModel> GetBudgetBreakdownByUser(long userId);
        Task RemoveBudgetBreakdownByUserId(long userId);
        Task UpdateBudgetBreakdownByUserId(BudgetBreakdownModel budgetBreakdownModel);
    }
}
