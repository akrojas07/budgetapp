using BudgetManagement.API.Models.IncomeModels;
using BudgetManagement.Domain.Models;
using BudgetManagement.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagement.API.Controllers
{
    [Route("api/budget/income")]
    [ApiController]
    public class BudgetIncomeController : ControllerBase
    {
        private readonly IBudgetIncomeServices _incomeServices;

        public BudgetIncomeController(IBudgetIncomeServices incomeServices)
        {
            _incomeServices = incomeServices;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewIncome(AddNewIncomeRequest newIncomeRequest)
        {
            if(newIncomeRequest == null)
            {
                return StatusCode(400, "Income details not provided");
            }

            try
            {
                BudgetIncomeModel coreIncomeModel = new BudgetIncomeModel()
                {
                    UserId = (long)newIncomeRequest.UserId,
                    IncomeAmount = (decimal)newIncomeRequest.IncomeAmount,
                    IncomeType = newIncomeRequest.IncomeType

                };

                await _incomeServices.AddNewIncome(coreIncomeModel);
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
        [Route("{userId}")]
        public async Task<IActionResult> GetAllIncomeByUserId(long userId)
        {
            if(userId == 0)
            {
                return StatusCode(400, "Bad Request");
            }

            try
            {
                return Ok(await _incomeServices.GetAllIncomeByUserId(userId));
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

        [HttpDelete]
        public async Task<IActionResult> RemoveIncome(RemoveIncomeRequest removeIncome)
        {
            if(removeIncome == null)
            {
                return StatusCode(400, "Bad Request");
            }

            try
            {
                await _incomeServices.RemoveIncome((long)removeIncome.IncomeId);
                return Ok();
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

        [HttpPatch]
        public async Task<IActionResult> UpdateIncome(UpdateIncomeRequest updateIncome)
        {
            if(updateIncome ==null)
            {
                return StatusCode(400, "Bad Request");
            }

            try
            {
                await _incomeServices.UpdateIncome((long)updateIncome.IncomeId, (decimal)updateIncome.IncomeAmount);
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

        [HttpPut]
        public async Task<IActionResult> UpsertIncomes([FromBody]UpsertIncomesRequest upsertIncomes)
        {
            if(upsertIncomes.Incomes.Count <=0)
            {
                return StatusCode(400, "Bad Request");
            }

            try
            {
                List<BudgetIncomeModel> budgetIncomes = new List<BudgetIncomeModel>();

                foreach(var upsertIncome in upsertIncomes.Incomes)
                {
                    BudgetIncomeModel coreIncomeModel = new BudgetIncomeModel() 
                    { 
                        Id = upsertIncome.Id,
                        UserId = upsertIncome.UserId,
                        IncomeAmount = upsertIncome.Amount,
                        IncomeType = upsertIncome.IncomeType
                    };
                }

                await _incomeServices.UpsertIncomes(budgetIncomes);
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
