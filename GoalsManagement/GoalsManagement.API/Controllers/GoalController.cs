using GoalsManagement.API.Models;
using GoalsManagement.Domain.Interfaces;
using GoalsManagement.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GoalsManagement.API.Controllers
{
    [Route("api/goals")]
    [ApiController]
    public class GoalController : ControllerBase
    {
        private readonly IGoalServices _goalServices;

        public GoalController(IGoalServices goalServices)
        {
            _goalServices = goalServices;
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetGoals(long userId)
        {
            try
            {
                if(userId <= 0)
                {
                    throw new ArgumentException("User ID Not provided");
                }
                return Ok(await _goalServices.GetGoalsService(userId));
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

        [HttpPut]
        public async Task<IActionResult> UpsertGoals([FromBody] UpsertGoalsRequest upsertGoals)
        {
            try
            {
                
                if(upsertGoals == null || upsertGoals.UpsertGoals[0].GoalName == null)
                {
                    throw new ArgumentException("Bad Request");
                }

                List<GoalModel> coreGoals = new List<GoalModel>();

                foreach(var upsertGoal in upsertGoals.UpsertGoals)
                {
                    if(upsertGoal.UserId <=0)
                    {
                        throw new Exception("User not found");
                    }
                    GoalModel coreGoal = new GoalModel()
                    {
                        Id = upsertGoal.Id,
                        UserId = upsertGoal.UserId,
                        Amount = upsertGoal.GoalAmount,
                        TargetAmount = upsertGoal.TargetAmount,
                        GoalName = upsertGoal.GoalName,
                        GoalSummary = upsertGoal.GoalSummary,
                        StartDate = upsertGoal.StartDate,
                        EndDate = upsertGoal.EndDate
                    };

                    coreGoals.Add(coreGoal);
                }

                await _goalServices.UpsertGoalsService(coreGoals);
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
    }
}
