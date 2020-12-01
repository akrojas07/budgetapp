using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoalsManagement.Persistence.Interfaces;
using Moq;
using NUnit.Framework;
using GoalsManagement.Persistence.Entities;
using GoalsManagement.Domain.Services;
using GoalsManagement.Domain.Models;

namespace GoalsManagement.Test.Domain_Tests
{
    [TestFixture]
    public class GoalServicesTest
    {
        private Mock<IGoalsRepository> _goalsRepository;

        [SetUp]
        public void Setup()
        {
            _goalsRepository = new Mock<IGoalsRepository>();
        }

        [Test]
        public async Task Test_GetGoalsService_Success()
        {
            _goalsRepository.Setup(g => g.GetGoals(It.IsAny<long>()))
                .ReturnsAsync(new List<Goal>());

            var goalsService = new GoalServices(_goalsRepository.Object);
            await goalsService.GetGoalsService(1);

            _goalsRepository.Verify(g => g.GetGoals(It.IsAny<long>()), Times.Once);

        }

        [Test]
        public void Test_GetGoalsService_ArgumentException()
        {
            _goalsRepository.Setup(g => g.GetGoals(It.IsAny<long>()))
                .ReturnsAsync(new List<Goal>());

            var goalsService = new GoalServices(_goalsRepository.Object);
            Assert.ThrowsAsync<ArgumentException>(() => goalsService.GetGoalsService(0));

            _goalsRepository.Verify(g => g.GetGoals(It.IsAny<long>()), Times.Never);
        }

        [Test]
        public void Test_GetGoalsService_Exception()
        {
            _goalsRepository.Setup(g => g.GetGoals(It.IsAny<long>()));

            var goalsService = new GoalServices(_goalsRepository.Object);
            Assert.ThrowsAsync<Exception>(() => goalsService.GetGoalsService(1));

            _goalsRepository.Verify(g => g.GetGoals(It.IsAny<long>()), Times.Once);
        }

        [Test]
        public async Task Test_UpsertGoalsService_Success()
        {
            _goalsRepository.Setup(g => g.UpsertGoals(It.IsAny<List<Goal>>()))
                .Returns(Task.CompletedTask);

            var goalsService = new GoalServices(_goalsRepository.Object);
            await goalsService.UpsertGoalsService(new List<GoalModel>() {
            new GoalModel()
            {
                Id = 1,
                UserId = 5,
                Amount = 5,
                TargetAmount = 50,
                GoalName= "Name",
                GoalSummary = "Summary",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now

            },
            new GoalModel()
                {
                    Id = 1,
                    UserId = 5,
                    Amount = 5,
                    TargetAmount = 50,
                    GoalName= "Name",
                    GoalSummary = "Summary",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now
                }
            }
            );

            _goalsRepository.Verify(g => g.UpsertGoals(It.IsAny<List<Goal>>()), Times.Once);
        }

        [Test]
        public void Test_UpsertGoalsService_BadArgument()
        {
            var goalsService = new GoalServices(_goalsRepository.Object);
            Assert.ThrowsAsync<ArgumentException>(() => goalsService.UpsertGoalsService(null));

            _goalsRepository.Verify(g => g.UpsertGoals(It.IsAny<List<Goal>>()), Times.Never);
        }

        [Test]
        public void Test_UpsertGoalsService_Exception()
        {
            var goalsService = new GoalServices(_goalsRepository.Object);
            Assert.ThrowsAsync<Exception>(() => goalsService.UpsertGoalsService(new List<GoalModel>() {
            new GoalModel()
            {
                Id = 1,
                Amount = 5,
                TargetAmount = 50,
                GoalName= "Name",
                GoalSummary = "Summary",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now

            },
            new GoalModel()
                {
                    Id = 1,
                    UserId = 5,
                    Amount = 5,
                    TargetAmount = 50,
                    GoalName= "Name",
                    GoalSummary = "Summary",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now
                }
            }
            ));

            _goalsRepository.Verify(g => g.UpsertGoals(It.IsAny<List<Goal>>()), Times.Never);
        }
    }
}
