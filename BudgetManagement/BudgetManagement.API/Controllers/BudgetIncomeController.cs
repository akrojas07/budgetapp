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
    public class BudgetIncomeController : ControllerBase
    {
        private IBudgetIncomeServices _incomeServices;

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
                    UserId = newIncomeRequest.UserId,
                    IncomeAmount = newIncomeRequest.IncomeAmount,
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
        public async Task<IActionResult> GetAllIncomeByUserId(GetAllIncomeByUserIdRequest getAllIncome)
        {
            if(getAllIncome == null)
            {
                return StatusCode(400, "Bad Request");
            }

            try
            {
                return Ok(await _incomeServices.GetAllIncomeByUserId(getAllIncome.UserId));
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
                await _incomeServices.RemoveIncome(removeIncome.IncomeId);
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
                await _incomeServices.UpdateIncome(updateIncome.IncomeId, updateIncome.IncomeAmount);
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
