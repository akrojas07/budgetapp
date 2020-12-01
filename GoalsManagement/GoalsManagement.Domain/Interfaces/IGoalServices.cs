using GoalsManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoalsManagement.Domain.Interfaces
{
    public interface IGoalServices
    {
        Task<List<GoalModel>> GetGoalsService(long userId);
        Task UpsertGoalsService(List<GoalModel> goals);
    }
}
