using BudgetManagement.Persistence.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManagement.Persistence.Repositories.Interfaces
{
    public interface IBudgetExpensesRepository
    {
        Task<List<BudgetExpenses>> GetAllExpensesByUserId(long userId);

        Task AddNewExpense(BudgetExpenses budgetExpenses);
        Task UpdateExpense(long expenseId, decimal expenseAmount);
        Task RemoveExpense(long expenseId);
        Task<BudgetExpenses> GetExpenseByExpenseId(long expenseId);
    }
}
