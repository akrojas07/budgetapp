using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BudgetManagement.Persistence.Repositories.Entities;
using BudgetManagement.Persistence.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace BudgetManagement.Persistence.Repositories
{
    public class BudgetBreakdownRepository : IBudgetBreakdownRepository
    {
        private readonly string _connectionString;

        public BudgetBreakdownRepository(IConfiguration config)
        {
            _connectionString = config.GetSection("ConnectionString:BudgetDb").Value;
        }

        /// <summary>
        /// Method to add new budget breakdown by User ID
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="budgetType"></param>
        /// <param name="expenses"></param>
        /// <param name="savings"></param>
        /// <returns>Completed Task</returns>
        public async Task AddNewBudgetBreakdownByUserId(BudgetBreakdown budgetBreakdown)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@UserId", budgetBreakdown.UserId);
                parameters.Add("@BudgetType", budgetBreakdown.BudgetType);
                parameters.Add("@Expenses", budgetBreakdown.Expenses);
                parameters.Add("@Savings", budgetBreakdown.Savings);

                await connection.ExecuteAsync("dbo.AddNewBudgetBreakdownByUserId", parameters, commandType: CommandType.StoredProcedure);

                connection.Close(); 
            }
        }

        /// <summary>
        /// Method to pull budget breakdown information by User ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Budget Breakdown object </returns>
        public async Task<BudgetBreakdown> GetBudgetBreakdownByUserId(long userId)
        {

            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameter = new DynamicParameters();
                parameter.Add("@UserId", userId);

                var dbBudgetBreakdown = await connection.QueryFirstOrDefaultAsync<BudgetBreakdown>("dbo.GetAllBudgetBreakdownByUserId", parameter, commandType: CommandType.StoredProcedure);

                connection.Close();

                return dbBudgetBreakdown;
            }

        }

        /// <summary>
        /// Method to pull budget type by User ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Budget Type as string </returns>
        public async Task<string> GetBudgetTypeByUserId(long userId)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameter = new DynamicParameters();
                parameter.Add("@UserId", userId);

                var dbBudgetType = await connection.QueryFirstOrDefaultAsync<string>("dbo.GetBudgetTypeByUserId", parameter, commandType: CommandType.StoredProcedure);

                connection.Close();

                return dbBudgetType; 
            }
        }

        /// <summary>
        /// Method to remove budget breakdown by User ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Completed task </returns>
        public async Task RemoveBudgetBreakdownByUserId(long userId)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameter = new DynamicParameters();
                parameter.Add("@UserId", userId);

                await connection.ExecuteAsync("dbo.RemoveBudgetBreakdownByUser", parameter, commandType: CommandType.StoredProcedure);

                connection.Close();
            }
        }

        /// <summary>
        /// Method to update existing budget breakdown by User ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Completed Task</returns>
        public async Task UpdateBudgetBreakdownByUserId(BudgetBreakdown budgetBreakdown)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@UserId", budgetBreakdown.UserId);
                parameters.Add("@BudgetType", budgetBreakdown.BudgetType);
                parameters.Add("@Expenses", budgetBreakdown.Expenses);
                parameters.Add("@Savings", budgetBreakdown.Savings);

                await connection.ExecuteAsync("dbo.UpdateBudgetBreakdownByUser", parameters, commandType: CommandType.StoredProcedure);

                connection.Close();
            }
        }
    }
}
