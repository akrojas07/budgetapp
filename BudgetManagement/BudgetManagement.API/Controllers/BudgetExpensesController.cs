using BudgetManagement.API.Models.ExpenseModels;
using BudgetManagement.Domain.Models;
using BudgetManagement.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagement.API.Controllers
{
    [Route("budget/expenses")]
    [ApiController]
    /*[Authorize]*/
    public class BudgetExpensesController: ControllerBase
    {
        private readonly IBudgetExpensesServices _expenseServices;
        private readonly IConfiguration _configuration;

        public BudgetExpensesController(IBudgetExpensesServices expenseServices/*, IConfiguration config*/)
        {
            //_configuration = config;
            _expenseServices = expenseServices;
        }

        [HttpPost]
        [Route("addnewexpense")]
        public async Task<IActionResult> AddNewExpense([FromBody]AddNewExpenseRequest newExpenseRequest)
        {
            if(newExpenseRequest == null)
            {
                return StatusCode(400,"Expense not provided");
            }

            try
            {
                BudgetExpensesModel coreExpensesModel = new BudgetExpensesModel()
                {
                    UserId = (long)newExpenseRequest.UserId,
                    ExpenseAmount = (decimal)newExpenseRequest.ExpenseAmount,
                    ExpenseType = newExpenseRequest.ExpenseType
                };

                await _expenseServices.AddNewExpense(coreExpensesModel);

                return StatusCode(201);
            }
            catch(ArgumentException ae)
            {
                return StatusCode(400, ae.Message);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]

        public async Task<IActionResult> GetExpensesByUserId(GetAllExpensesByUserIdRequest userId)
        {
            if(userId == null)
            {
                return StatusCode(400, "Bad Request");
            }

            try
            {
                return Ok(await _expenseServices.GetExpensesByUserId((long)userId.UserId));
                
            }
            catch(ArgumentException ae)
            {
                return StatusCode(400, ae.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> RemoveExpense(RemoveExpenseRequest removeExpense)
        {
            if(removeExpense == null)
            {
                return StatusCode(400, "Bad Request");
            }

            try
            {
                await _expenseServices.RemoveExpense((long)removeExpense.ExpenseId);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateExpense(UpdateExpenseRequest expenseRequest)
        {
            if(expenseRequest == null)
            {
                return StatusCode(400, "Bad Request");
            }

            try
            {
                await _expenseServices.UpdateExpense((long)expenseRequest.ExpenseId, (decimal)expenseRequest.ExpenseAmount);
                return Ok();
            }
            catch (ArgumentException ae)
            {
                return StatusCode(400, ae.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
    }
}
