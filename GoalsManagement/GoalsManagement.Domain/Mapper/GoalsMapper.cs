using System;
using System.Collections.Generic;
using System.Text;

using CoreGoalModel = GoalsManagement.Domain.Models.GoalModel;
using DbGoalEntity = GoalsManagement.Persistence.Entities.Goal;

namespace GoalsManagement.Domain.Mapper
{
    public static class GoalsMapper
    {
        public static DbGoalEntity CoreToDbGoalEntity(CoreGoalModel coreModel)
        {
            DbGoalEntity dbGoal = new DbGoalEntity()
            {
                Id = coreModel.Id,
                UserId = coreModel.UserId,
                Amount = coreModel.Amount,
                TargetAmount = coreModel.TargetAmount,
                GoalName = coreModel.GoalName,
                GoalSummary = coreModel.GoalSummary,
                StartDate = coreModel.StartDate,
                EndDate = coreModel.EndDate
            };

            return dbGoal;
        }

        public static CoreGoalModel DbToCoreGoalModel(DbGoalEntity dbGoal)
        {
            CoreGoalModel coreModel = new CoreGoalModel()
            {
                Id = dbGoal.Id,
                UserId = dbGoal.UserId,
                Amount = (decimal)dbGoal.Amount,
                TargetAmount = (decimal)dbGoal.TargetAmount,
                GoalName = dbGoal.GoalName,
                GoalSummary = dbGoal.GoalSummary,
                StartDate = (DateTime)dbGoal.StartDate,
                EndDate = (DateTime) dbGoal.EndDate
            };

            return coreModel;
        }
    }
}
