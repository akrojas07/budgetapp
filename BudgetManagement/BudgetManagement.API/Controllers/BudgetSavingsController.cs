﻿using BudgetManagement.API.Models.SavingsModels;
using BudgetManagement.Domain.Models;
using BudgetManagement.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagement.API.Controllers
{
    [Route("budget/savings")]
    [ApiController]
    public class BudgetSavingsController: ControllerBase
    {
        private readonly IBudgetSavingsServices _savingsServices;

        public BudgetSavingsController(IBudgetSavingsServices savingsServices)
        {
            _savingsServices = savingsServices;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewSaving(AddNewSavingRequest savingsRequest)
        {
            if(savingsRequest == null)
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
            catch(ArgumentException ae)
            {
                return StatusCode(400, ae.Message);
            }
            catch(Exception e)
            {
                return StatusCode(500,e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSavingsByUserId(GetAllSavingsByUserIdRequest getRequest)
        {
            if(getRequest == null)
            {
                return StatusCode(400, "Bad Request");
            }

            try
            {
                return Ok(await _savingsServices.GetAllSavingsByUserId((long)getRequest.UserId));
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