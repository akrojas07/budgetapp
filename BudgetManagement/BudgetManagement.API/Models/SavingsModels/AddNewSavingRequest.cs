using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagement.API.Models.SavingsModels
{
    public class AddNewSavingRequest: BaseClassRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Savings Type required")]
        public string SavingsType { get; set; }

        [Required(ErrorMessage = "Savings Amount required")]
        public decimal SavingsAmount { get; set; }
    }
}
