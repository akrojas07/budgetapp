﻿using BudgetManagement.API.Models.BudgetBreakdownModels;
using BudgetManagement.Domain.Models;
using BudgetManagement.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagement.API.Controllers
{
    [Route("budget/breakdown")]
    [ApiController]
    public class BudgetBreakdownController:ControllerBase
    {
        private readonly IBudgetBreakdownServices _budgetBreakdownServices;

        public BudgetBreakdownController(IBudgetBreakdownServices breakdownServices)
        {
            _budgetBreakdownServices = breakdownServices;
        }

        [HttpPost]
        [Route("newbreakdown")]
        public async Task<IActionResult> AddNewBudgetBreakdown([FromBody] AddNewBudgetBreakdownRequest newBreakdownRequest)
        {
            if(newBreakdownRequest == null)
            {
                return StatusCode(400, "Budget Breakdown not provided");
            }

            try
            {
                BudgetBreakdownModel coreBreakdownModel = new BudgetBreakdownModel()
                {
                    UserId = (long)newBreakdownRequest.UserId,
                    BudgetType = newBreakdownRequest.BudgetType,
                    ExpensesBreakdown = newBreakdownRequest.ExpensesBreakdown,
                    SavingsBreakdown = newBreakdownRequest.SavingsBreakdown
                };

                await _budgetBreakdownServices.AddNewBudgetBreakdownByUserId(coreBreakdownModel);

                return StatusCode(201);
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

        [HttpGet]
        [Route("breakdown")]
        public async Task<IActionResult> GetBudgetBreakdownByUserId(GetBudgetBreakdownByUserIdRequest breakdownUserId)
        {
            if(breakdownUserId == null)
            {
                return StatusCode(400, "Budget Breakdown not provided");
            }
            try
            {
                return Ok(await _budgetBreakdownServices.GetBudgetBreakdownByUser((long)breakdownUserId.UserId));
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

        [HttpGet]
        [Route("budgettype")]

        public async Task<IActionResult> GetBudgetTypeByUserId(GetBudgetTypeByUserId budgetTypeUserId)
        {
            if (budgetTypeUserId == null)
            {
                return StatusCode(400, "Budget Breakdown not provided");
            }
            try
            {
                return Ok(await _budgetBreakdownServices.GetBudgetTypeByUserId((long) budgetTypeUserId.UserId));
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

        [HttpDelete]
        [Route("removebreakdown")]
        public async Task<IActionResult> RemoveBudgetBreakdownByUserId(RemoveBudgetBreakdownRequest removeBreakdown)
        {
            if (removeBreakdown == null)
            {
                return StatusCode(400, "Budget Breakdown not provided");
            }
            try
            {
                await _budgetBreakdownServices.RemoveBudgetBreakdownByUserId((long)removeBreakdown.UserId);
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

        [HttpPatch]
        [Route("updatebreakdown")]
        public async Task<IActionResult> UpdateBudgetBreakdownByUser(UpdateBudgetBreakdownRequest updateBreakdown)
        {
            if (updateBreakdown == null)
            {
                return StatusCode(400, "Budget Breakdown not provided");
            }
            try
            {
                await _budgetBreakdownServices.UpdateBudgetBreakdownByUserId(new BudgetBreakdownModel() 
                { 
                    Id = updateBreakdown.Id,
                    UserId = (long)updateBreakdown.UserId,
                    BudgetType = updateBreakdown.BudgetType,
                    ExpensesBreakdown = updateBreakdown.ExpensesBreakdown,
                    SavingsBreakdown = updateBreakdown.SavingsBreakdown
                });

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
