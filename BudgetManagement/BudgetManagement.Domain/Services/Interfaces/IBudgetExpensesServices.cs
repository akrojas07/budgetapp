using BudgetManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManagement.Domain.Services.Interfaces
{
    public interface IBudgetExpensesServices
    {
        Task<List<BudgetExpensesModel>> GetExpensesByUserId(long userId);
        Task AddNewExpense(BudgetExpensesModel expensesModel);
        Task UpdateExpense(long expenseId, decimal expenseAmount);
        Task RemoveExpense(long expenseId);
    }
}
