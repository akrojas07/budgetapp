using GoalsManagement.Persistence.Entities;
using GoalsManagement.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalsManagement.Persistence.Repositories
{
    public class GoalsRepository : IGoalsRepository
    {
        public async Task<List<Goal>> GetGoals(long userId)
        {
            List<Goal> dbGoals = new List<Goal>();
            using (var context = new BudgetAppContext())
            {
                dbGoals = await context.Goals.Where(g => g.UserId == userId)
                    .ToListAsync();
            }
            return dbGoals;
        }


        public async Task UpsertGoals(List<Goal> goals)
        {
            using (var context = new BudgetAppContext())
            {
                var goalComparer = new GoalComparer();
                var dbGoals = await context.Goals.Where(g => g.UserId == goals[0].UserId).ToListAsync();

                var goalsToAdd = goals.Except(dbGoals, goalComparer).ToList();
                var goalsToUpdate = goals.Intersect(dbGoals).ToList();
                var goalsToDelete = dbGoals.Except(goals, goalComparer).ToList();

                foreach (var goal in goalsToAdd)
                {
                    goal.CreatedDate = DateTime.Now;
                    goal.UpdatedDate = DateTime.Now;
                    context.Add(goal);
                }

                foreach(var goal in goalsToUpdate)
                {
                    var dbGoal = await context.Goals.FirstOrDefaultAsync(g => g.Id == goal.Id);
                    dbGoal.UserId = goal.UserId;
                    dbGoal.GoalName = goal.GoalName;
                    dbGoal.GoalSummary = goal.GoalSummary;
                    dbGoal.StartDate = goal.StartDate;
                    dbGoal.EndDate = goal.EndDate;
                    dbGoal.Amount = goal.Amount;
                    dbGoal.TargetAmount = goal.TargetAmount;
                    dbGoal.UpdatedDate = DateTime.Now;
                }

                context.Goals.RemoveRange(goalsToDelete);

                await context.SaveChangesAsync();
            }
        }
    }
}
