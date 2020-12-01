using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoalsManagement.API.Models
{
    public class UpsertGoalsRequest
    {
        public List<UpsertGoal> UpsertGoals {get; set;} 
    }

    public class UpsertGoal
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string GoalName { get; set; }
        public string GoalSummary { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal GoalAmount { get; set; }
        public decimal TargetAmount { get; set; }
    }
}
