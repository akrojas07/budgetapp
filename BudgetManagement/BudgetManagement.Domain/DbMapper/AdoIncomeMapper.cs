using System;
using System.Collections.Generic;
using System.Text;
using CoreIncomeModel = BudgetManagement.Domain.Models.BudgetIncomeModel;
using DbIncomeEntity = BudgetManagement.Persistence.Repositories.Entities.BudgetIncome;

namespace BudgetManagement.Domain.DbMapper
{
    public static class AdoIncomeMapper
    {
        /// <summary>
        /// Mapper method to convert database income entity to core income model
        /// </summary>
        /// <param name="incomeEntity"></param>
        /// <returns>Budget Income Model Object</returns>
        public static  CoreIncomeModel DbEntityToCoreModel(DbIncomeEntity incomeEntity)
        {
            CoreIncomeModel coreModel = new CoreIncomeModel()
            {
                Id = incomeEntity.Id,
                IncomeAmount = incomeEntity.IncomeAmount,
                IncomeType = incomeEntity.IncomeType,
                UserId = incomeEntity.UserId
            };

            return coreModel;
        }

        /// <summary>
        /// Mapper method to convert new Budget Income Model to db income entity
        /// Does not map income id 
        /// </summary>
        /// <param name="incomeModel"></param>
        /// <returns>Budget Income Entity </returns>
        public static  DbIncomeEntity CoreModelToDbEntityNew(CoreIncomeModel incomeModel)
        {
            DbIncomeEntity dbEntity = new DbIncomeEntity()
            {
                UserId = incomeModel.UserId,
                IncomeAmount = incomeModel.IncomeAmount,
                IncomeType = incomeModel.IncomeType
            };

            return dbEntity;
        }

        /// <summary>
        /// Mapper method to convert existing Budget income model to db income entity 
        /// Maps income id 
        /// </summary>
        /// <param name="incomeModel"></param>
        /// <returns>Budget Income Entity</returns>
        public static DbIncomeEntity CoreModelToDbEntityExisting(CoreIncomeModel incomeModel)
        {
            DbIncomeEntity dbEntity = new DbIncomeEntity()
            {
                Id = incomeModel.Id,
                UserId = incomeModel.UserId,
                IncomeType = incomeModel.IncomeType,
                IncomeAmount = incomeModel.IncomeAmount
            };

            return dbEntity;
        }
    }
}
