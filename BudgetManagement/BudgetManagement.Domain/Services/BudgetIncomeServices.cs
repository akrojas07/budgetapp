using BudgetManagement.Domain.Models;
using BudgetManagement.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BudgetManagement.Domain.DbMapper;
using BudgetManagement.Persistence.Repositories.Interfaces;

namespace BudgetManagement.Domain.Services
{
    public class BudgetIncomeServices : IBudgetIncomeServices
    {
        private readonly IBudgetIncomeRepository _incomeRepository;

        public BudgetIncomeServices(IBudgetIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }
        /// <summary>
        /// Service Method to add new income record
        /// </summary>
        /// <param name="incomeModel"></param>
        /// <returns>Task Complete</returns>
        public async Task AddNewIncome(BudgetIncomeModel incomeModel)
        {
            //convert core income model to database income entity 
            var dbIncomeEntity = AdoIncomeMapper.CoreModelToDbEntityNew(incomeModel);

            //validate conversion was successful
            if(dbIncomeEntity == null)
            {
                throw new Exception("Income could not be added");
            }

            //add new income
            await _incomeRepository.AddNewIncome(dbIncomeEntity);
        }

        /// <summary>
        /// Service method to pull income for specific user by user id 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of Budget Income Model objects</returns>
        public async Task<List<BudgetIncomeModel>> GetAllIncomeByUserId(long userId)
        {
            //create empty list of core budget income objects
            List<BudgetIncomeModel> coreIncomeList = new List<BudgetIncomeModel>();

            //pull all income by user id from db
            var dbIncomeList = await _incomeRepository.GetAllIncomeByUserId(userId);

            //convert from db income entity to core income model 
            foreach(var income in dbIncomeList)
            {
                coreIncomeList.Add(AdoIncomeMapper.DbEntityToCoreModel(income));
            }

            return coreIncomeList;

        }

        /// <summary>
        /// Service method to remove income record
        /// </summary>
        /// <param name="incomeId"></param>
        /// <returns>Task Complete</returns>
        public async Task RemoveIncome(long incomeId)
        {
            //pull income object
            var incomeEntity = await _incomeRepository.GetIncomeByIncomeId(incomeId);

            //validate income is not empty
            if(incomeEntity == null)
            {
                throw new Exception("Unable to find income");
            }

            await _incomeRepository.RemoveIncome(incomeId);
        }

        /// <summary>
        /// Service method to update existing income record
        /// </summary>
        /// <param name="incomeId"></param>
        /// <param name="incomeAmount"></param>
        /// <returns>Task Complete</returns>
        public async Task UpdateIncome(long incomeId, decimal incomeAmount)
        {
            //pull income object
            var incomeEntity = await _incomeRepository.GetIncomeByIncomeId(incomeId);

            //validate income is not empty
            if (incomeEntity == null)
            {
                throw new Exception("Unable to find income");
            }

            await _incomeRepository.UpdateIncome(incomeId, incomeAmount);
        }
    }
}
