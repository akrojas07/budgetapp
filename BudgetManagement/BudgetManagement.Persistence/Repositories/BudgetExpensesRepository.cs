using BudgetManagement.Persistence.Repositories.Entities;
using BudgetManagement.Persistence.Repositories.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;
using System.Linq;





namespace BudgetManagement.Persistence.Repositories
{
    public class BudgetExpensesRepository : IBudgetExpensesRepository
    {
        private readonly string _connectionString;

        public BudgetExpensesRepository(IConfiguration config)
        {
            _connectionString = config.GetSection("ConnectionString:BudgetDb").Value;
        }

        /// <summary>
        /// Method to add new expense object
        /// </summary>
        /// <param name="budgetExpenses"></param>
        /// <returns> Task Complete  </returns>
        public async Task AddNewExpense(BudgetExpenses budgetExpenses)
        {
            using(var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var parameter = new DynamicParameters();
                parameter.Add("@UserId", budgetExpenses.UserId);
                parameter.Add("@ExpenseType", budgetExpenses.ExpenseType);
                parameter.Add("@ExpenseAmount", budgetExpenses.ExpenseAmount);

                await conn.ExecuteAsync("dbo.AddNewExpense", parameter, commandType: CommandType.StoredProcedure);

                conn.Close();
            }
        }

        /// <summary>
        /// Method to pull all expenses by user id 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of Budget Expense Objects </returns>
        public async Task<List<BudgetExpenses>> GetAllExpensesByUserId(long userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameter = new DynamicParameters();
                parameter.Add("@UserId", userId);

                var budgetExpenses = await connection.QueryAsync<BudgetExpenses>("dbo.GetExpensesByUserId", parameter, commandType: CommandType.StoredProcedure);

                connection.Close();

                return budgetExpenses.ToList();
            }
        }

        /// <summary>
        /// Method to pull single expense record by expense id 
        /// </summary>
        /// <param name="expenseId"></param>
        /// <returns>Budget Expenses object</returns>
        public async Task<BudgetExpenses> GetExpenseByExpenseId(long expenseId)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@ExpenseId", expenseId);

                var budgetExpense = await connection.QueryFirstOrDefaultAsync("dbo.GetExpenseByExpenseId", parameters, commandType: CommandType.StoredProcedure);

                connection.Close();

                return budgetExpense;
            }
        }

        /// <summary>
        /// Method to remove existing expense
        /// </summary>
        /// <param name="expenseId"></param>
        /// <returns> Task Complete  </returns>

        public async Task RemoveExpense(long expenseId)
        {
           using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameter = new DynamicParameters();
                parameter.Add("@ExpenseId", expenseId);

                await connection.ExecuteAsync("dbo.RemoveExpense", parameter, commandType: CommandType.StoredProcedure);

                connection.Close();
            }
        }

        /// <summary>
        /// Method to update existing expense 
        /// </summary>
        /// <param name="expenseId"></param>
        /// <param name="expenseAmount"></param>
        /// <returns> Task Complete </returns>

        public async Task UpdateExpense(long expenseId, decimal expenseAmount)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameter = new DynamicParameters();
                parameter.Add("@ExpenseId", expenseId);
                parameter.Add("@ExpenseAmount", expenseAmount);

                await connection.ExecuteAsync("dbo.UpdateExpense", parameter, commandType: CommandType.StoredProcedure);

                connection.Close();
            }
        }
    }
}
