using BudgetManagement.Persistence.Repositories.Entities;
using BudgetManagement.Persistence.Repositories.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManagement.Persistence.Repositories
{
    public class BudgetIncomeRepository: IBudgetIncomeRepository
    {
        private readonly string _connectionString;

        public BudgetIncomeRepository(IConfiguration config)
        {
            _connectionString = config.GetSection("ConnectionString:BudgetDb").Value;           
        }

        /// <summary>
        /// Method to return all incomes entered by user 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of Budget Income Object</returns>
        public async Task<List<BudgetIncome>> GetAllIncomeByUserId(long userId)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var param = new DynamicParameters();
                param.Add("@UserId", userId);

                //query asyncy returns a list, query first or default returns a single item, execute executes
                var incomes = await connection.QueryAsync<BudgetIncome>("dbo.GetIncomeByUserId", param, commandType: CommandType.StoredProcedure);

                connection.Close();

                return incomes.ToList();
            }
        }

        /// <summary>
        /// Method to update income  
        /// </summary>
        /// <param name="incomeId"></param>
        /// <param name="incomeAmount"></param>
        /// <returns> void </returns>

        public async Task UpdateIncome(long incomeId, decimal incomeAmount)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@IncomeId", incomeId);
                parameters.Add("@IncomeAmount", incomeAmount);

                await connection.ExecuteAsync("dbo.UpdateIncome", parameters, commandType: CommandType.StoredProcedure);

                connection.Close();

            }
        }

        /// <summary>
        /// Method to add new budget income object
        /// </summary>
        /// <param name="budgetIncome"></param>
        /// <returns>void </returns>
        public async Task AddNewIncome(BudgetIncome budgetIncome) 
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@UserId", budgetIncome.UserId);
                parameters.Add("@IncomeType", budgetIncome.IncomeType);
                parameters.Add("@IncomeAmount", budgetIncome.IncomeAmount);

                await connection.ExecuteAsync("dbo.AddNewIncome", parameters, commandType: CommandType.StoredProcedure);

                connection.Close();
            }
        
        }

        /// <summary>
        /// Method to remove existing income 
        /// </summary>
        /// <param name="incomeId"></param>
        /// <returns>void </returns>
        public async Task RemoveIncome(long incomeId)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameter = new DynamicParameters();
                parameter.Add("@IncomeId", incomeId);

                await connection.ExecuteAsync("dbo.RemoveIncome", parameter, commandType:CommandType.StoredProcedure);


                connection.Close();
            }
        }
    }
}
