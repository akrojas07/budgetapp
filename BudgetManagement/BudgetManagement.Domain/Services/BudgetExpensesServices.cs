using BudgetManagement.Domain.Models;
using BudgetManagement.Domain.Services.Interfaces;
using BudgetManagement.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BudgetManagement.Domain.DbMapper;

namespace BudgetManagement.Domain.Services
{
    public class BudgetExpensesServices : IBudgetExpensesServices
    {
        private readonly IBudgetExpensesRepository _expensesRepository;

        public BudgetExpensesServices(IBudgetExpensesRepository expensesRepository)
        {
            _expensesRepository = expensesRepository;
        }

        /// <summary>
        /// Service method to add new expense record
        /// </summary>
        /// <param name="expensesModel"></param>
        /// <returns>Task complete</returns>
        public async Task AddNewExpense(BudgetExpensesModel expensesModel)
        {
            if(expensesModel == null)
            {
                throw new ArgumentException("Expense not found");
            }

            //convert core expense model to db expense entity 
            var dbExpenseEntity = AdoExpensesMapper.CoreModelToDbEntityNew(expensesModel);

            await _expensesRepository.AddNewExpense(dbExpenseEntity);
        }

        /// <summary>
        /// Service method to get all expense records by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List Budget Expense Model objects </returns>
        public async Task<List<BudgetExpensesModel>> GetExpensesByUserId(long userId)
        {
            //create empty core expense list
            List<BudgetExpensesModel> coreExpenseList = new List<BudgetExpensesModel>();

            //pull db expense entity list
            var dbExpenseList = await _expensesRepository.GetAllExpensesByUserId(userId);

            if(dbExpenseList == null)
            {
                throw new Exception("Expenses not found");
            }

            //convert from db entity to core model
            foreach(var expense in dbExpenseList)
            {
                coreExpenseList.Add(AdoExpensesMapper.DbEntityToCoreModel(expense));
            }

            return coreExpenseList;
        }

        /// <summary>
        /// Service method to remove existing expense record
        /// </summary>
        /// <param name="expenseId"></param>
        /// <returns>Task Complete</returns>
        public async Task RemoveExpense(long expenseId)
        {
            //pull expense record 
            var expenseEntity = await _expensesRepository.GetExpenseByExpenseId(expenseId);

            //validate expense exists
            if (expenseEntity == null)
            {
                throw new Exception("Expense not found");
            }

            await _expensesRepository.RemoveExpense(expenseId);
        }

        /// <summary>
        /// Service method to update existing expense record
        /// </summary>
        /// <param name="expenseId"></param>
        /// <param name="expenseAmount"></param>
        /// <returns>Task complete</returns>
        public async Task UpdateExpense(long expenseId, decimal expenseAmount)
        {
            //pull expense record 
            var expenseEntity = await _expensesRepository.GetExpenseByExpenseId(expenseId);

            //validate expense exists
            if (expenseEntity == null)
            {
                throw new Exception("Expense not found");
            }

            await _expensesRepository.UpdateExpense(expenseId, expenseAmount);
        }
    }
}
