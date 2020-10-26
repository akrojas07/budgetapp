using System;
using System.Collections.Generic;
using System.Text;
using CoreModel = BudgetManagement.Domain.Models.BudgetBreakdownModel;
using DbEntity = BudgetManagement.Persistence.Repositories.Entities.BudgetBreakdown;

namespace BudgetManagement.Domain.DbMapper
{
    public static class AdoBudgetBreakdownMapper
    {
        public static CoreModel DbEntityToCoreModel(DbEntity dbEntity)
        {
            CoreModel coreModel = new CoreModel()
            {
                Id = dbEntity.Id,
                UserId = dbEntity.UserId, 
                BudgetType = dbEntity.BudgetType,
                ExpensesBreakdown = dbEntity.ExpensesBreakdown,
                SavingsBreakdown = dbEntity.SavingsBreakdown
            };

            return coreModel;
        }

        public static DbEntity NewCoreModelToDbEntity(CoreModel coreModel)
        {
            DbEntity dbEntity = new DbEntity()
            {
                UserId = coreModel.UserId,
                BudgetType = coreModel.BudgetType,
                ExpensesBreakdown = coreModel.ExpensesBreakdown,
                SavingsBreakdown = coreModel.SavingsBreakdown
            };

            return dbEntity;
        }

        public static DbEntity ExistingCoreModelToDbEntity(CoreModel coreModel)
        {
            DbEntity dbEntity = new DbEntity()
            {
                Id = coreModel.Id, 
                UserId = coreModel.UserId,
                BudgetType = coreModel.BudgetType,
                ExpensesBreakdown = coreModel.ExpensesBreakdown,
                SavingsBreakdown = coreModel.SavingsBreakdown
            };

            return dbEntity;
        }
    }
}
