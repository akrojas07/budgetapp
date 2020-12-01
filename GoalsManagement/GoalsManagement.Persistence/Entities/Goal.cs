using System;
using System.Collections.Generic;

#nullable disable

namespace GoalsManagement.Persistence.Entities
{
    public partial class Goal
    {
        //Scaffold-DbContext "Server=DESKTOP-LQ9NL1I;Database=BudgetApp;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -f -Tables dbo.goals, dbo.userAccount
        //use to generate entities in EF
        //need to install Microsoft.EntityFrameworkCore.Tools package in order to use this
        public long Id { get; set; }
        public long UserId { get; set; }
        public string GoalName { get; set; }
        public string GoalSummary { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public decimal? TargetAmount { get; set; }

        public virtual UserAccount User { get; set; }
    }

    class GoalComparer: IEqualityComparer<Goal>
    {
        public bool Equals(Goal firstGoal, Goal secondGoal)
        {
            if(Object.ReferenceEquals(firstGoal, secondGoal))
            {
                return true;
            }

            if(object.ReferenceEquals(firstGoal, null) || object.ReferenceEquals(secondGoal, null))
            {
                return false;
            }

            return firstGoal.Id == secondGoal.Id;
        }

        public int GetHashCode(Goal goal)
        {
            if (Object.ReferenceEquals(goal, null))
            {
                return 0;
            }

            int hashGoalId = goal.Id == 0 ? 0 : goal.Id.GetHashCode();

            return hashGoalId;
        }
    }
}
