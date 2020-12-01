using GoalsManagement.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoalsManagement.Persistence.Interfaces
{
    public interface IGoalsRepository
    {
        Task<List<Goal>> GetGoals(long userId);
        Task UpsertGoals(List<Goal> goals);


    }
}
