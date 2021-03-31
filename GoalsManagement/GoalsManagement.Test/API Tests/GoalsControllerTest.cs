using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GoalsManagement.Domain.Interfaces;
using Moq;
using NUnit.Framework;
using GoalsManagement.Domain.Models;
using GoalsManagement.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using GoalsManagement.API.Models;

namespace GoalsManagement.Test.API_Tests
{
    [TestFixture]
    public class GoalsControllerTest
    {
        private Mock<IGoalServices> _goalServices; 

        [SetUp]
        public void Setup()
        {
            _goalServices = new Mock<IGoalServices>();
        }

        [Test]
        public async Task Test_GetGoalsController_Success()
        {
            _goalServices.Setup(g => g.GetGoalsService(It.IsAny<long>()))
                .ReturnsAsync(new List<GoalModel>());

            var controller = new GoalController(_goalServices.Object);
            var response = await controller.GetGoals(1);

            Assert.NotNull(response);
            Assert.AreEqual(200, ((OkObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_GetGoalsController_BadArgument()
        {
            _goalServices.Setup(g => g.GetGoalsService(It.IsAny<long>()))
                .ReturnsAsync(new List<GoalModel>());

            var controller = new GoalController(_goalServices.Object);
            var response = await controller.GetGoals(0);

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_GetGoalsController_Exception()
        {
            _goalServices.Setup(g => g.GetGoalsService(It.IsAny<long>()))
                .ThrowsAsync(new Exception("Goals not found"));

            var controller = new GoalController(_goalServices.Object);
            var response = await controller.GetGoals(1);

            Assert.NotNull(response);
            Assert.AreEqual(500, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_UpsertGoalsController_Success()
        {
            _goalServices.Setup(g => g.UpsertGoalsService(It.IsAny<List<GoalModel>>()))
                .Returns(Task.CompletedTask);

            var controller = new GoalController(_goalServices.Object);
            var response = await controller.UpsertGoals(new UpsertGoalsRequest()
            {
               UpsertGoals = new List<UpsertGoal>()
                {
                   new UpsertGoal()
                   {
                       Id = 5,
                       UserId = 5,
                       GoalAmount = 50,
                       TargetAmount = 500,
                       GoalName = "Goal Name",
                       GoalSummary = "Goal Summary",
                       StartDate = DateTime.Now,
                       EndDate = DateTime.Now
                   }
                }
            });

            Assert.NotNull(response);
            Assert.AreEqual(200, ((OkResult)response).StatusCode);
        }

        [Test]
        public async Task Test_UpsertGoalsController_BadArgument()
        {
            _goalServices.Setup(g => g.UpsertGoalsService(It.IsAny<List<GoalModel>>()))
                .Returns(Task.CompletedTask);

            var controller = new GoalController(_goalServices.Object);
            var response = await controller.UpsertGoals(null);

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_UpsertGoalsController_Exception()
        {
            _goalServices.Setup(g => g.UpsertGoalsService(It.IsAny<List<GoalModel>>()))
                .Returns(Task.CompletedTask);

            var controller = new GoalController(_goalServices.Object);
            var response = await controller.UpsertGoals(new UpsertGoalsRequest());

            Assert.NotNull(response);
            Assert.AreEqual(500, ((ObjectResult)response).StatusCode);
        }
    }
}
