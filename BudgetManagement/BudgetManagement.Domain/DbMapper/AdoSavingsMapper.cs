using System;
using System.Collections.Generic;
using System.Text;

using CoreSavingsModel = BudgetManagement.Domain.Models.BudgetSavingsModel;
using DbSavingsEntity = BudgetManagement.Persistence.Repositories.Entities.BudgetSavings;

namespace BudgetManagement.Domain.DbMapper
{
    public static class AdoSavingsMapper
    {
        /// <summary>
        /// Mapper method to convert new savings record from core savings model to database savings entity
        /// Does not map savings id 
        /// </summary>
        /// <param name="coreModel"></param>
        /// <returns>Database savings entity object</returns>
        public static DbSavingsEntity CoreToDbEntityNew(CoreSavingsModel coreModel)
        {
            DbSavingsEntity dbEntity = new DbSavingsEntity()
            {
                UserId = coreModel.UserId,
                SavingsAmount = coreModel.SavingsAmount,
                SavingsType = coreModel.SavingsType
            };

            return dbEntity;
        }

        /// <summary>
        /// Mapper method to convert existing savings record from core savings model to database savings entity
        /// Maps savings id 
        /// </summary>
        /// <param name="coreModel"></param>
        /// <returns>database savings entity object</returns>
        public static DbSavingsEntity CoreToDbEntityExisting(CoreSavingsModel coreModel)
        {
            DbSavingsEntity dbEntity = new DbSavingsEntity()
            {
                Id = coreModel.Id,
                UserId = coreModel.UserId,
                SavingsAmount = coreModel.SavingsAmount,
                SavingsType = coreModel.SavingsType
            };

            return dbEntity;
        }

        /// <summary>
        /// Mapper method to convert core savings model object to db savings entity object 
        /// </summary>
        /// <param name="dbEntity"></param>
        /// <returns>Core Savings Model object </returns>
        public static CoreSavingsModel DbToCoreModel(DbSavingsEntity dbEntity)
        {
            CoreSavingsModel coreModel = new CoreSavingsModel()
            {
                Id = dbEntity.Id,
                UserId =dbEntity.UserId,
                SavingsAmount = dbEntity.SavingsAmount,
                SavingsType = dbEntity.SavingsType
            };

            return coreModel;

        }
    }
}
