using BudgetManagement.Domain.Models;
using BudgetManagement.Domain.Services.Interfaces;
using BudgetManagement.Persistence.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using BudgetManagement.Domain.DbMapper;

namespace BudgetManagement.Domain.Services
{
    public class BudgetBreakdownServices : IBudgetBreakdownServices
    {
        private readonly IBudgetBreakdownRepository _budgetBreakdownRepository;

        public BudgetBreakdownServices(IBudgetBreakdownRepository budgetBreakdownRepository)
        {
            _budgetBreakdownRepository = budgetBreakdownRepository;
        }

        /// <summary>
        /// Method to add new budget breakdown by user 
        /// </summary>
        /// <param name="budgetBreakdownModel"></param>
        /// <returns>Completed task </returns>
        public async Task AddNewBudgetBreakdownByUserId(BudgetBreakdownModel budgetBreakdownModel)
        {
            if(budgetBreakdownModel == null)
            {
                throw new ArgumentException("Budget Breakdown not found");
            }
            if(budgetBreakdownModel.SavingsBreakdown + budgetBreakdownModel.ExpensesBreakdown > 100)
            {
                throw new Exception("Breakdown percents cannot exceed 100%");
            }

            var dbBreakdown = AdoBudgetBreakdownMapper.NewCoreModelToDbEntity(budgetBreakdownModel);
            await _budgetBreakdownRepository.AddNewBudgetBreakdownByUserId(dbBreakdown);
        }

        /// <summary>
        /// Method to pull budget breakdown record by user id 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Budget Breakdown model </returns>
        public async Task<BudgetBreakdownModel> GetBudgetBreakdownByUser(long userId)
        {
            if(userId == 0)
            {
                throw new ArgumentException("User not found");
            }

            var dbBudgetBreakdown = await _budgetBreakdownRepository.GetBudgetBreakdownByUserId(userId);

            var coreBreakdown = AdoBudgetBreakdownMapper.DbEntityToCoreModel(dbBudgetBreakdown);

            return coreBreakdown;
        }

        /// <summary>
        /// Method to pull budget type by user id 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Budget type as string </returns>
        public async Task<string> GetBudgetTypeByUserId(long userId)
        {
            if (userId == 0)
            {
                throw new ArgumentException("User not found");
            }

            var budgetType = await _budgetBreakdownRepository.GetBudgetTypeByUserId(userId);
            return budgetType;
        }

        /// <summary>
        /// Method to remove existing budget breakdown by user id 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Completed task </returns>
        public async Task RemoveBudgetBreakdownByUserId(long userId)
        {
            //pull existing breakdown record
            var dbBudgetBreakdown = await _budgetBreakdownRepository.GetBudgetBreakdownByUserId(userId);
            
            //throw exception if not valid 
            if (dbBudgetBreakdown == null)
            {
                throw new ArgumentException("Budget Breakdown not found");
            }

            await _budgetBreakdownRepository.RemoveBudgetBreakdownByUserId(userId);
        }

        /// <summary>
        /// Method to update existing budget breakdown by user id 
        /// </summary>
        /// <param name="budgetBreakdownModel"></param>
        /// <returns>Completed task</returns>
        public async Task UpdateBudgetBreakdownByUserId(BudgetBreakdownModel budgetBreakdownModel)
        {
            //validate inputs
            if (budgetBreakdownModel == null)
            {
                throw new ArgumentException("Budget Breakdown not found");
            }

            //validate inputs don't exceed 100
            if (budgetBreakdownModel.SavingsBreakdown + budgetBreakdownModel.ExpensesBreakdown > 100)
            {
                throw new Exception("Breakdown percents cannot exceed 100%");
            }

            //pull existing budget breakdown record 
            var dbBudgetBreakdown = await _budgetBreakdownRepository.GetBudgetBreakdownByUserId(budgetBreakdownModel.UserId);

            //validate that the correct breakdown was pulled
            if(budgetBreakdownModel.Id != dbBudgetBreakdown.Id)
            {
                throw new Exception("Budget Breakdown records do not match");
            }

            //throw exception if not valid 
            if (dbBudgetBreakdown == null)
            {
                throw new Exception("Budget Breakdown not found");
            }


            //map core model to db entity 
            var dbBudgetBreakdownEntity = AdoBudgetBreakdownMapper.ExistingCoreModelToDbEntity(budgetBreakdownModel);

            await _budgetBreakdownRepository.UpdateBudgetBreakdownByUserId(dbBudgetBreakdownEntity); 
        }
    }
}
