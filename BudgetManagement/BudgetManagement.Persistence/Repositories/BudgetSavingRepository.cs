using BudgetManagement.Persistence.Repositories.Entities;
using BudgetManagement.Persistence.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using System.Data;

namespace BudgetManagement.Persistence.Repositories
{
    public class BudgetSavingRepository : IBudgetSavingsRepository
    {
        private readonly string _connectionString;

        public BudgetSavingRepository(IConfiguration config)
        {
            _connectionString = config.GetSection("ConnectionString:BudgetDb").Value;
        }
        /// <summary>
        /// Method to add new saving object 
        /// </summary>
        /// <param name="budgetSavings"></param>
        /// <returns>void </returns>
        public async Task AddNewSaving(BudgetSavings budgetSavings)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameter = new DynamicParameters();
                parameter.Add("@UserId", budgetSavings.UserId);
                parameter.Add("@SavingsType", budgetSavings.SavingsType);
                parameter.Add("@SavingsAmount", budgetSavings.SavingsAmount);

                await connection.ExecuteAsync("dbo.AddNewSaving", parameter, commandType: CommandType.StoredProcedure);

                connection.Close();
            }
        }

        /// <summary>
        /// Method to Get all savings by user id 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of Budget Savings Object </returns>
        public async Task<List<BudgetSavings>> GetAllSavingsByUserId(long userId)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameter = new DynamicParameters();
                parameter.Add("@UserId", userId);

                var budgetSavings = await connection.QueryAsync<BudgetSavings>("dbo.GetSavingsByUserId", parameter, commandType: CommandType.StoredProcedure);

                connection.Close();

                return budgetSavings.ToList();
            }
        }

        /// <summary>
        /// Method to pull single savings record by saving id 
        /// </summary>
        /// <param name="savingId"></param>
        /// <returns>Budget Savings Object</returns>
        public async Task<BudgetSavings> GetSavingBySavingId(long savingId)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameter = new DynamicParameters();
                parameter.Add("@SavingId", savingId);

                var budgetSaving = await connection.QueryFirstOrDefaultAsync("dbo.GetSavingBySavingId", parameter, commandType: CommandType.StoredProcedure);

                connection.Close();

                return budgetSaving;
            }
        }

        /// <summary>
        /// Method to remove existing saving item
        /// </summary>
        /// <param name="savingId"></param>
        /// <returns> void </returns>

        public async Task RemoveSaving(long savingId)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameter = new DynamicParameters();
                parameter.Add("@SavingsId", savingId);

                await connection.ExecuteAsync("dbo.RemoveSaving", parameter, commandType: CommandType.StoredProcedure);

                connection.Close();
            }
        }
        /// <summary>
        /// Method to update existing saving item 
        /// </summary>
        /// <param name="savingId"></param>
        /// <param name="savingAmount"></param>
        /// <returns>void </returns>

        public async Task UpdateSaving(long savingId, decimal savingAmount)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@SavingsId", savingId);
                parameters.Add("@SavingsAmount", savingAmount);

                await connection.ExecuteAsync("dbo.UpdateSaving", parameters, commandType: CommandType.StoredProcedure);


                connection.Close();
            }
        }
    }
}
