using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagement.API.Models.SavingsModels
{
    public class UpdateSavingRequest:BaseSavingsRequest
    {
        [Required(ErrorMessage ="SavingsAmount Required")]
        public decimal? SavingsAmount { get; set; }
    }
}
