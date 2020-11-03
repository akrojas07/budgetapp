using BudgetManagement.Domain.Models;
using BudgetManagement.Domain.Services.Interfaces;
using BudgetManagement.Persistence.Repositories.Interfaces;
using BudgetManagement.Persistence.Repositories.Entities; 
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mapper = BudgetManagement.Domain.DbMapper.AdoSavingsMapper;

namespace BudgetManagement.Domain.Services
{
    public class BudgetSavingsServices : IBudgetSavingsServices
    {
        private readonly IBudgetSavingsRepository _savingsRepository;

        public BudgetSavingsServices(IBudgetSavingsRepository savingsRepository)
        {
            _savingsRepository = savingsRepository;
        }

        /// <summary>
        /// Service Method to add a new saving item
        /// </summary>
        /// <param name="budgetSavingsModel"></param>
        /// <returns>Task Complete </returns>
        public async Task AddNewSaving(BudgetSavingsModel budgetSavingsModel)
        {
            if(budgetSavingsModel == null)
            {
                throw new ArgumentException("Savings not provided");
            }

            //map domain to db budgetSavingsModel
            var dbSavingsEntity = Mapper.CoreToDbEntityNew(budgetSavingsModel);
            await _savingsRepository.AddNewSaving(dbSavingsEntity);

        }

        /// <summary>
        /// Service Method to pull all saving items for user by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of Budget Savings Model objects</returns>

        public async Task<List<BudgetSavingsModel>> GetAllSavingsByUserId(long userId)
        {
            //create empty list for Core savings objects 
            List<BudgetSavingsModel> coreSavingsModel = new List<BudgetSavingsModel>();

            //pull dabatase savings list by user id
            var savingsList = await _savingsRepository.GetAllSavingsByUserId(userId);

            if(savingsList == null)
            {
                throw new Exception("Savings not found");
            }

            //convert savings list from db savings entity to core savings model and store in core savings model list
            foreach (var saving in savingsList)
            {
                coreSavingsModel.Add(Mapper.DbToCoreModel(saving));
            }

            return coreSavingsModel;
        }

        /// <summary>
        /// Service Method to remove a specific saving line item
        /// </summary>
        /// <param name="savingId"></param>
        /// <returns>Task Complete </returns>
        public async Task RemoveSaving(long savingId)
        {
            //pull database savings entity 
            var savingsEntity = await _savingsRepository.GetSavingBySavingId(savingId);

            //validate it is not empty
            if(savingsEntity == null)
            {
                throw new Exception("Saving Record does not exist");
            }

            //if not empty, call remove saving repo method
            await _savingsRepository.RemoveSaving(savingId);

        }

        /// <summary>
        /// Service method to update a specific savings line item by savings id 
        /// </summary>
        /// <param name="savingId"></param>
        /// <returns>Task Complete </returns>
        public async Task UpdateSaving(long savingId, decimal savingsAmount)
        {
            //pull database savings entity 
            var savingsEntity = await _savingsRepository.GetSavingBySavingId(savingId);

            //validate entity exists
            if(savingsEntity == null)
            {
                throw new Exception("Saving record does not exist");
            }

            //if exists, call update saving repo method
            await _savingsRepository.UpdateSaving(savingId, savingsAmount);
        }

        /// <summary>
        /// Method to insert / update / remove savings records 
        /// </summary>
        /// <param name="budgetSavings"></param>
        /// <returns>Task Complete</returns>
        public async Task UpsertSavings(List<BudgetSavingsModel> budgetSavings)
        {
            if(budgetSavings.Count <= 0)
            {
                throw new ArgumentException("Savings not provided");
            }

            List<BudgetSavings> dbSavings = new List<BudgetSavings>();

            foreach(var budgetSaving in budgetSavings)
            {
                dbSavings.Add(Mapper.CoreToDbEntityExisting(budgetSaving));
            }

            await _savingsRepository.UpsertSavings(dbSavings);
            
        }
    }
}
