using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagement.API.Models.SavingsModels
{
    public class UpsertSavingsRequest
    {
        public List<Saving> Savings { get; set; }
    }

    public class Saving
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string SavingType { get; set; }
        public decimal Amount { get; set; }
    }
}
