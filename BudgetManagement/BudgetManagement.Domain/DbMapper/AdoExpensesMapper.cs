using System;
using System.Collections.Generic;
using System.Text;
using CoreExpenseModel = BudgetManagement.Domain.Models.BudgetExpensesModel;
using DbExpenseEntity = BudgetManagement.Persistence.Repositories.Entities.BudgetExpenses;

namespace BudgetManagement.Domain.DbMapper
{
    public static class AdoExpensesMapper
    {
        /// <summary>
        /// Mapper method to convert database expense entity to core expense model 
        /// </summary>
        /// <param name="expenseEntity"></param>
        /// <returns>Core Expense model</returns>
        public static CoreExpenseModel DbEntityToCoreModel(DbExpenseEntity expenseEntity)
        {
            CoreExpenseModel coreModel = new CoreExpenseModel()
            {
                Id = expenseEntity.Id,
                UserId = expenseEntity.UserId,
                ExpenseAmount = expenseEntity.ExpenseAmount,
                ExpenseType = expenseEntity.ExpenseType
            };
            return coreModel;
        }

        /// <summary>
        /// Mapper method to convert core expense model to db expense entity
        /// Does not map id
        /// </summary>
        /// <param name="coreModel"></param>
        /// <returns>Db Expense entity</returns>
        public static DbExpenseEntity CoreModelToDbEntityNew(CoreExpenseModel coreModel)
        {
            DbExpenseEntity dbEntity = new DbExpenseEntity()
            {
                UserId = coreModel.UserId,
                ExpenseType = coreModel.ExpenseType,
                ExpenseAmount = coreModel.ExpenseAmount
            };
            return dbEntity;
        }

        /// <summary>
        /// Mapper method to convert core expense model to db expense entity
        /// Maps id 
        /// </summary>
        /// <param name="coreModel"></param>
        /// <returns>Db Expense entity</returns>
        public static DbExpenseEntity CoreModelToDbEntityExisting(CoreExpenseModel coreModel)
        {
            DbExpenseEntity dbEntity = new DbExpenseEntity()
            {
                Id = coreModel.Id,
                UserId = coreModel.UserId,
                ExpenseAmount = coreModel.ExpenseAmount,
                ExpenseType = coreModel.ExpenseType
            };
            return dbEntity;
        }
    }
}
