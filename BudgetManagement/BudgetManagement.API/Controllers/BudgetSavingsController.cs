using BudgetManagement.API.Models.SavingsModels;
using BudgetManagement.Domain.Models;
using BudgetManagement.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagement.API.Controllers
{
    [Route("api/budget/savings")]
    [ApiController]
    public class BudgetSavingsController : ControllerBase
    {
        private readonly IBudgetSavingsServices _savingsServices;

        public BudgetSavingsController(IBudgetSavingsServices savingsServices)
        {
            _savingsServices = savingsServices;
        }

        [HttpPut]
        public async Task<IActionResult> UpsertSavings([FromBody] UpsertSavingsRequest upsertSavings)
        {
            if(upsertSavings.Savings.Count <= 0)
            {
                return StatusCode(400, "Bad Request");
            }
            try
            {
                List<BudgetSavingsModel> budgetSavings = new List<BudgetSavingsModel>();
                foreach(var upsertSaving in upsertSavings.Savings)
                {
                    BudgetSavingsModel coreSavingsModel = new BudgetSavingsModel()
                    {
                        Id = upsertSaving.Id,
                        UserId = upsertSaving.UserId,
                        SavingsAmount = upsertSaving.Amount,
                        SavingsType = upsertSaving.SavingType
                    };

                    budgetSavings.Add(coreSavingsModel);
                }

                await _savingsServices.UpsertSavings(budgetSavings);
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

        [HttpPost]
        public async Task<IActionResult> AddNewSaving(AddNewSavingRequest savingsRequest)
        {
            if (savingsRequest == null)
            {
                return StatusCode(400, "Bad Request");
            }

            try
            {
                BudgetSavingsModel savingsModel = new BudgetSavingsModel()
                {
                    UserId = (long)savingsRequest.UserId,
                    SavingsAmount = (decimal)savingsRequest.SavingsAmount,
                    SavingsType = savingsRequest.SavingsType
                };

                await _savingsServices.AddNewSaving(savingsModel);

                return StatusCode(201);
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
        [Route("{userId}")]
        public async Task<IActionResult> GetAllSavingsByUserId(long userId)
        {
            if(userId == 0)
            {
                return StatusCode(400, "Bad Request");
            }

            try
            {
                return Ok(await _savingsServices.GetAllSavingsByUserId(userId));
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
        public async Task<IActionResult> RemoveSaving(RemoveSavingRequest removeSaving)
        {
            if(removeSaving == null)
            {
                return StatusCode(400, "Bad Request");
            }

            try
            {
                await _savingsServices.RemoveSaving((long)removeSaving.SavingsId);
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
        public async Task<IActionResult> UpdateSaving(UpdateSavingRequest updateSaving)
        {
            if(updateSaving == null)
            {
                return StatusCode(400, "Bad Request");
            }

            try
            {
                await _savingsServices.UpdateSaving((long)updateSaving.SavingsId, (decimal)updateSaving.SavingsAmount);
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
