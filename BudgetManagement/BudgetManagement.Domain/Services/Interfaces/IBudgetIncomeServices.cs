using BudgetManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManagement.Domain.Services.Interfaces
{
    public interface IBudgetIncomeServices
    {
        Task<List<BudgetIncomeModel>> GetAllIncomeByUserId(long userId);
        Task AddNewIncome(BudgetIncomeModel incomeModel);
        Task UpdateIncome(long incomeId, decimal incomeAmount);
        Task RemoveIncome(long incomeId);
        Task UpsertIncomes(List<BudgetIncomeModel> budgetIncomes);
    }
}
