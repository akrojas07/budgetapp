using BudgetManagement.Domain.Models;
using BudgetManagement.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BudgetManagement.Domain.DbMapper;
using BudgetManagement.Persistence.Repositories.Interfaces;
using BudgetManagement.Persistence.Repositories.Entities;

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
            //validate income was passed
            if(incomeModel == null)
            {
                throw new ArgumentException("Income not provided");
            }
            //convert core income model to database income entity 
            var dbIncomeEntity = AdoIncomeMapper.CoreModelToDbEntityNew(incomeModel);

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

            if(dbIncomeList == null)
            {
                throw new Exception("Income not found");
            }

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

        /// <summary>
        /// Method to insert / update / remove income records
        /// </summary>
        /// <param name="budgetIncomes"></param>
        /// <returns>Task Complete</returns>
        public async Task UpsertIncomes(List<BudgetIncomeModel> budgetIncomes)
        {
            if(budgetIncomes.Count <=0)
            {
                throw new Exception("Income not provided");
            }

            List<BudgetIncome> dbIncomes = new List<BudgetIncome>();

            foreach(var budgetIncome in budgetIncomes)
            {
                dbIncomes.Add(AdoIncomeMapper.CoreModelToDbEntityExisting(budgetIncome));
            }

            await _incomeRepository.UpsertIncomes(dbIncomes);
        }
    }
}
