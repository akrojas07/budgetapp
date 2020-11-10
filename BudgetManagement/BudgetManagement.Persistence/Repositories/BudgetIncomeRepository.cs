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

                //query asyncy returns a list, query first or default returns a single item, execute runs
                var incomes = await connection.QueryAsync<BudgetIncome>("dbo.GetIncomeByUserId", param, commandType: CommandType.StoredProcedure);

                connection.Close();

                return incomes.ToList();
            }
        }


        /// <summary>
        /// Method to insert/update/delete income record
        /// </summary>
        /// <param name="budgetIncomes"></param>
        /// <returns>Task Complete</returns>
        public async Task UpsertIncomes(List<BudgetIncome> budgetIncomes)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var dataTable = new DataTable();
                dataTable.Columns.Add("@UserId", typeof(long));
                dataTable.Columns.Add("@BudgetTypeId", typeof(long));
                dataTable.Columns.Add("@Type", typeof(string));
                dataTable.Columns.Add("@Amount", typeof(decimal));

                foreach(var budgetIncome in budgetIncomes)
                {
                    dataTable.Rows.Add(budgetIncome.UserId, budgetIncome.Id, budgetIncome.IncomeType, budgetIncome.IncomeAmount);
                }

                var parameter = new DynamicParameters();
                parameter.Add("@Incomes", dataTable.AsTableValuedParameter("[dbo].[BudgetInputsTableType]"));

                await connection.ExecuteAsync("dbo.UpsertIncomes", parameter, commandType: CommandType.StoredProcedure) ; 
                
                connection.Close(); 
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

        /// <summary>
        /// Method to pull single income record by income id 
        /// </summary>
        /// <param name="incomeId"></param>
        /// <returns>BudgetIncome object</returns>
        public async Task<BudgetIncome> GetIncomeByIncomeId(long incomeId)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameter = new DynamicParameters();
                parameter.Add("@IncomeId", incomeId);

                var budgetIncome = await connection.QueryFirstOrDefaultAsync<BudgetIncome>("dbo.GetIncomeByIncomeId", parameter, commandType: CommandType.StoredProcedure);

                connection.Close();

                return budgetIncome;
            }
        }
    }
}
