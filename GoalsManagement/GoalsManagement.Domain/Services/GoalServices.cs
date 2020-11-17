using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GoalsManagement.Domain.Interfaces;
using GoalsManagement.Domain.Models;
using GoalsManagement.Domain.Mapper;
using GoalsManagement.Persistence.Interfaces;
using GoalsManagement.Persistence.Entities;

namespace GoalsManagement.Domain.Services
{
    public class GoalServices : IGoalServices
    {
        private readonly IGoalsRepository _goalsRepository; 
        public GoalServices(IGoalsRepository goalsRepository) 
        {
            _goalsRepository = goalsRepository;
        }
        public async Task<List<GoalModel>> GetGoalsService(long userId)
        {
            try
            {
                if(userId <=0)
                {
                    throw new ArgumentException("User not found");
                }

                List<GoalModel> coreGoals = new List<GoalModel>();
                List<Goal> dbGoals = await _goalsRepository.GetGoals(userId);

                if(dbGoals == null || dbGoals.Count == 0)
                {
                    throw new Exception("No goals found");
                }

                foreach(var goal in dbGoals)
                {
                    if(goal.UserId == 0)
                    {
                        throw new ArgumentException("User not found");
                    }
                    coreGoals.Add(Mapper.GoalsMapper.DbToCoreGoalModel(goal));
                }

                return coreGoals;

            }
            catch(ArgumentException ae)
            {
                throw new ArgumentException(ae.Message);
            }
            catch(Exception e)
            {
                throw new Exception( e.Message);
            }
        }


        public async Task UpsertGoalsService(List<GoalModel> goals)
        {
            try
            {
                if(goals == null)
                {
                    throw new ArgumentException("Goals not entered");
                }

                List<Goal> dbGoals = new List<Goal>();

                foreach(var goal in goals)
                {
                    if(goal.UserId<= 0)
                    {
                        throw new Exception("Goals not found");
                    }
                    dbGoals.Add(Mapper.GoalsMapper.CoreToDbGoalEntity(goal));
                }

                await _goalsRepository.UpsertGoals(dbGoals);
            }
            catch (ArgumentException ae)
            {
                throw new ArgumentException(ae.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
