using System;
using System.Collections.Generic;
using System.Text;

namespace GoalsManagement.Domain.Models
{
    public class GoalModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string GoalName { get; set; }
        public string GoalSummary { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Amount { get; set; }
        public decimal TargetAmount { get; set; }

    }
}
