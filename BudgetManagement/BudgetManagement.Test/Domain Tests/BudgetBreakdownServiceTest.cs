using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;
using BudgetManagement.Domain.Services.Interfaces;
using System.Threading.Tasks;
using BudgetManagement.Domain.Models;
using BudgetManagement.Persistence.Repositories.Interfaces;
using BudgetManagement.Persistence.Repositories.Entities;
using BudgetManagement.Domain.Services;

namespace BudgetManagement.Test.Domain_Tests
{
    [TestFixture]
    public class BudgetBreakdownServiceTest
    {
        private Mock<IBudgetBreakdownRepository> _budgetBreakdownRepository;

        [SetUp]
        public void Setup()
        {
            _budgetBreakdownRepository = new Mock<IBudgetBreakdownRepository>();
        }

        [Test]
        public async Task Test_AddNewBudgetBreakdown_Success()
        {
            _budgetBreakdownRepository.Setup(b => b.AddNewBudgetBreakdownByUserId(It.IsAny<BudgetBreakdown>()))
                .Returns(Task.CompletedTask);

            var budgetBreakdownServices = new BudgetBreakdownServices(_budgetBreakdownRepository.Object);

            await budgetBreakdownServices.AddNewBudgetBreakdownByUserId(new BudgetBreakdownModel()
            {
                UserId = 1,
                BudgetType = "zbb",
                ExpensesBreakdown = .25m,
                SavingsBreakdown = .25m,
            });

            _budgetBreakdownRepository.Verify(b => b.AddNewBudgetBreakdownByUserId(It.IsAny<BudgetBreakdown>()), Times.Once);

        }

        [Test]
        public void Test_AddNewBudgetBreakdown_Fail_ArgumentException()
        {
            _budgetBreakdownRepository.Setup(b => b.AddNewBudgetBreakdownByUserId(It.IsAny<BudgetBreakdown>()))
                .Returns(Task.CompletedTask);

            var budgetBreakdownServices = new BudgetBreakdownServices(_budgetBreakdownRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => budgetBreakdownServices.AddNewBudgetBreakdownByUserId(null));

            _budgetBreakdownRepository.Verify(b => b.AddNewBudgetBreakdownByUserId(It.IsAny<BudgetBreakdown>()), Times.Never);
        }

        [Test]
        public void Test_AddNewBudgetBreakdown_Fail_Exception()
        {
            _budgetBreakdownRepository.Setup(b => b.AddNewBudgetBreakdownByUserId(It.IsAny<BudgetBreakdown>()))
                .Throws<Exception>();
            var budgetBreakdownServices = new BudgetBreakdownServices(_budgetBreakdownRepository.Object);

            Assert.ThrowsAsync<Exception>(() => budgetBreakdownServices.AddNewBudgetBreakdownByUserId(new BudgetBreakdownModel()
            {
                UserId = 1,
                BudgetType = "zbb",
                ExpensesBreakdown = .25m,
                SavingsBreakdown = .25m,
            }));
        
            _budgetBreakdownRepository.Verify(b => b.AddNewBudgetBreakdownByUserId(It.IsAny<BudgetBreakdown>()), Times.Once);

        }

        [Test]
        public async Task Test_GetBudgetBreakdownByUser_Success()
        {
            _budgetBreakdownRepository.Setup(b => b.GetBudgetBreakdownByUserId(It.IsAny<long>()))
                .ReturnsAsync(new BudgetBreakdown());

            var breakdownServices = new BudgetBreakdownServices(_budgetBreakdownRepository.Object);

            await breakdownServices.GetBudgetBreakdownByUser(1);

            _budgetBreakdownRepository.Verify(b => b.GetBudgetBreakdownByUserId(It.IsAny<long>()), Times.Once);
        }

        [Test]
        public void Test_GetBudgetBreakdownByUser_Fail_ArgumentException()
        {
            _budgetBreakdownRepository.Setup(b => b.GetBudgetBreakdownByUserId(It.IsAny<long>()))
                .ReturnsAsync(new BudgetBreakdown()); 

            var breakdownServices = new BudgetBreakdownServices(_budgetBreakdownRepository.Object);

            Assert.ThrowsAsync<ArgumentException>( () => breakdownServices.GetBudgetBreakdownByUser(0));

            _budgetBreakdownRepository.Verify(b => b.GetBudgetBreakdownByUserId(It.IsAny<long>()), Times.Never);
        }

        [Test]
        public void Test_GetBudgetBreakdownByUser_Fail_Exception()
        {
            _budgetBreakdownRepository.Setup(b => b.GetBudgetBreakdownByUserId(It.IsAny<long>()))
                .Throws<Exception>(); 

            var breakdownServices = new BudgetBreakdownServices(_budgetBreakdownRepository.Object);

            Assert.ThrowsAsync<Exception>(() => breakdownServices.GetBudgetBreakdownByUser(1));

            _budgetBreakdownRepository.Verify(b => b.GetBudgetBreakdownByUserId(It.IsAny<long>()), Times.Once);
        }

        [Test]
        public async Task Test_GetBudgetTypeByUserId_Success()
        {
            _budgetBreakdownRepository.Setup(b => b.GetBudgetTypeByUserId(It.IsAny<long>()))
                .ReturnsAsync("string");
            
            var breakdownServices = new BudgetBreakdownServices(_budgetBreakdownRepository.Object);
            await breakdownServices.GetBudgetTypeByUserId(1);

            _budgetBreakdownRepository.Verify(b => b.GetBudgetTypeByUserId(It.IsAny<long>()), Times.Once);
        }

        [Test]
        public void Test_GetBudgetTypeByUserId_Fail_ArgumentException()
        {
            _budgetBreakdownRepository.Setup(b => b.GetBudgetTypeByUserId(It.IsAny<long>()))
                .ReturnsAsync("string");

            var breakdownServices = new BudgetBreakdownServices(_budgetBreakdownRepository.Object);
            Assert.ThrowsAsync<ArgumentException>(() =>  breakdownServices.GetBudgetTypeByUserId(0));

            _budgetBreakdownRepository.Verify(b => b.GetBudgetTypeByUserId(It.IsAny<long>()), Times.Never);
        }

        [Test]
        public void Test_GetBudgetTypeByUserId_Fail_Exception()
        {
            _budgetBreakdownRepository.Setup(b => b.GetBudgetTypeByUserId(It.IsAny<long>()))
                .Throws<Exception>();

            var breakdownServices = new BudgetBreakdownServices(_budgetBreakdownRepository.Object);
            Assert.ThrowsAsync<Exception>(() => breakdownServices.GetBudgetTypeByUserId(1));

            _budgetBreakdownRepository.Verify(b => b.GetBudgetTypeByUserId(It.IsAny<long>()), Times.Once);
        }

        [Test]
        public async Task Test_RemoveBudgetBreakdownByUserId_Success()
        {
            _budgetBreakdownRepository.Setup(b => b.GetBudgetBreakdownByUserId(It.IsAny<long>()))
                .ReturnsAsync(new BudgetBreakdown());
            _budgetBreakdownRepository.Setup(b => b.RemoveBudgetBreakdownByUserId(It.IsAny<long>()))
                .Returns(Task.CompletedTask);

            var breakdownServices = new BudgetBreakdownServices(_budgetBreakdownRepository.Object);
            await breakdownServices.RemoveBudgetBreakdownByUserId(1);

            _budgetBreakdownRepository.Verify(b => b.RemoveBudgetBreakdownByUserId(It.IsAny<long>()), Times.Once);
        }

        [Test]
        public void Test_RemoveBudgetBreakdownByUserId_Fail_ArgumentException()
        {
            _budgetBreakdownRepository.Setup(b => b.GetBudgetBreakdownByUserId(It.IsAny<long>()));
            _budgetBreakdownRepository.Setup(b => b.RemoveBudgetBreakdownByUserId(It.IsAny<long>()))
                .Returns(Task.CompletedTask);

            var breakdownServices = new BudgetBreakdownServices(_budgetBreakdownRepository.Object);
            Assert.ThrowsAsync<ArgumentException>(() => breakdownServices.RemoveBudgetBreakdownByUserId(0));

            _budgetBreakdownRepository.Verify(b => b.RemoveBudgetBreakdownByUserId(It.IsAny<long>()), Times.Never);
        }

        [Test]
        public void Test_RemoveBudgetBreakdownByUserId_Fail_Exception()
        {
            _budgetBreakdownRepository.Setup(b => b.GetBudgetBreakdownByUserId(It.IsAny<long>()))
                .ReturnsAsync(new BudgetBreakdown());
            _budgetBreakdownRepository.Setup(b => b.RemoveBudgetBreakdownByUserId(It.IsAny<long>()))
                .Throws<Exception>();
            
            var breakdownServices = new BudgetBreakdownServices(_budgetBreakdownRepository.Object);
            Assert.ThrowsAsync<Exception>(() => breakdownServices.RemoveBudgetBreakdownByUserId(1));

            _budgetBreakdownRepository.Verify(b => b.RemoveBudgetBreakdownByUserId(It.IsAny<long>()), Times.Once);
        }

        [Test]
        public async Task Test_UpdateBudgetBreakdownByUserId_Success()
        {
            _budgetBreakdownRepository.Setup(b => b.GetBudgetBreakdownByUserId(It.IsAny<long>()))
                .ReturnsAsync(new BudgetBreakdown());
            _budgetBreakdownRepository.Setup(b => b.UpdateBudgetBreakdownByUserId(It.IsAny<BudgetBreakdown>()))
                .Returns(Task.CompletedTask);

            var breakdownServices = new BudgetBreakdownServices(_budgetBreakdownRepository.Object);
            await breakdownServices.UpdateBudgetBreakdownByUserId(new BudgetBreakdownModel() 
            {
                Id = 1,
                UserId = 18,
                BudgetType = "zbb", 
                ExpensesBreakdown = .35m,
                SavingsBreakdown = .15m 
            });

            _budgetBreakdownRepository.Verify(b => b.UpdateBudgetBreakdownByUserId(It.IsAny<BudgetBreakdown>()), Times.Once);
        }

        [Test]
        public void Test_UpdateBudgetBreakdownByUserId_Fail_ArgumentException()
        {
            _budgetBreakdownRepository.Setup(b => b.UpdateBudgetBreakdownByUserId(It.IsAny<BudgetBreakdown>()))
                .Returns(Task.CompletedTask);

            var breakdownServices = new BudgetBreakdownServices(_budgetBreakdownRepository.Object);
            Assert.ThrowsAsync<ArgumentException>(() =>  breakdownServices.UpdateBudgetBreakdownByUserId(null));

            _budgetBreakdownRepository.Verify(b => b.UpdateBudgetBreakdownByUserId(It.IsAny<BudgetBreakdown>()), Times.Never);
        }

        [Test]
        public void Test_UpdateBudgetBreakdownByUserId_Fail_Exception()
        {
            _budgetBreakdownRepository.Setup(b => b.GetBudgetBreakdownByUserId(It.IsAny<long>()));
            _budgetBreakdownRepository.Setup(b => b.UpdateBudgetBreakdownByUserId(It.IsAny<BudgetBreakdown>()))
                .Returns(Task.CompletedTask);

            var breakdownServices = new BudgetBreakdownServices(_budgetBreakdownRepository.Object);
            Assert.ThrowsAsync<Exception>(() => breakdownServices.UpdateBudgetBreakdownByUserId(new BudgetBreakdownModel()
            {
                Id = 1,
                UserId = 18,
                BudgetType = "zbb",
                ExpensesBreakdown = .35m,
                SavingsBreakdown = .15m
            }));

            _budgetBreakdownRepository.Verify(b => b.UpdateBudgetBreakdownByUserId(It.IsAny<BudgetBreakdown>()), Times.Never);
        }
    }
}
