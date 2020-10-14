using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BudgetManagement.Persistence.Repositories.Interfaces;
using BudgetManagement.Persistence.Repositories.Entities;
using BudgetManagement.Domain.Models;
using BudgetManagement.Domain.Services;

namespace BudgetManagement.Test.Domain_Tests
{
    [TestFixture]
    public class BudgetIncomeServicesTest
    {
        private Mock<IBudgetIncomeRepository> _incomeRepository;

        [SetUp]
        public void Setup()
        {
            _incomeRepository = new Mock<IBudgetIncomeRepository>();
        }

        [Test]
        public async Task Test_AddNewIncome_Success()
        {
            _incomeRepository.Setup(i => i.AddNewIncome(It.IsAny<BudgetIncome>()))
                .Returns(Task.CompletedTask);

            var incomeServices = new BudgetIncomeServices(_incomeRepository.Object);
            await incomeServices.AddNewIncome(new BudgetIncomeModel()
            { 
                UserId = 1,
                IncomeAmount = 5,
                IncomeType = "Pay Check"
            });

            _incomeRepository.Verify(i => i.AddNewIncome(It.IsAny<BudgetIncome>()), Times.Once);


        }

        [Test]
        public void Test_AddNewIncome_Fail_IncomeNotProvided()
        {
            var incomeServices = new BudgetIncomeServices(_incomeRepository.Object);
            Assert.ThrowsAsync<ArgumentException>(() => incomeServices.AddNewIncome(null));

            _incomeRepository.Verify(i => i.AddNewIncome(It.IsAny<BudgetIncome>()), Times.Never);
        }

        [Test]
        public async Task Test_GetAllIncomeByUserId_Success()
        {
            _incomeRepository.Setup(i => i.GetAllIncomeByUserId(It.IsAny<long>()))
                .ReturnsAsync(new List<BudgetIncome>());

            var incomeServices = new BudgetIncomeServices(_incomeRepository.Object);

            await incomeServices.GetAllIncomeByUserId(1);

            _incomeRepository.Verify(i => i.GetAllIncomeByUserId(It.IsAny<long>()), Times.Once);
        }

        [Test]
        public void Test_GetAllIncomeByUserId_Fail_IncomeRecordsNotFound()
        {
            _incomeRepository.Setup(i => i.GetAllIncomeByUserId(It.IsAny<long>()));

            var incomeServices = new BudgetIncomeServices(_incomeRepository.Object);

            Assert.ThrowsAsync<Exception>(() => incomeServices.GetAllIncomeByUserId(1));

            _incomeRepository.Verify(i => i.GetAllIncomeByUserId(It.IsAny<long>()), Times.Once);
        }

        [Test]
        public async Task Test_RemoveIncome_Success()
        {
            _incomeRepository.Setup(i => i.GetIncomeByIncomeId(It.IsAny<long>()))
                .ReturnsAsync(new BudgetIncome());

            _incomeRepository.Setup(i => i.RemoveIncome(It.IsAny<long>()))
                .Returns(Task.CompletedTask);

            var incomeServices = new BudgetIncomeServices(_incomeRepository.Object);
            await incomeServices.RemoveIncome(1);

            _incomeRepository.Verify(i => i.RemoveIncome(It.IsAny<long>()), Times.Once);
                       
        }

        [Test]
        public void Test_RemoveIncome_Fail_IncomeRecordNotFound()
        {
            _incomeRepository.Setup(i => i.GetIncomeByIncomeId(It.IsAny<long>()));

            var incomeServices = new BudgetIncomeServices(_incomeRepository.Object);

            Assert.ThrowsAsync<Exception>(() => incomeServices.RemoveIncome(2));

            _incomeRepository.Verify(i => i.RemoveIncome(It.IsAny<long>()), Times.Never);
        }

        [Test]
        public async Task Test_UpdateIncome_Success()
        {
            _incomeRepository.Setup(i => i.GetIncomeByIncomeId(It.IsAny<long>()))
                .ReturnsAsync(new BudgetIncome()) ;
            _incomeRepository.Setup(i => i.UpdateIncome(It.IsAny<long>(), It.IsAny<decimal>()))
                .Returns(Task.CompletedTask);

            var incomeServices = new BudgetIncomeServices(_incomeRepository.Object);
            await incomeServices.UpdateIncome(1,2);

            _incomeRepository.Verify(i => i.UpdateIncome(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
        }

        [Test]
        public void Test_UpdateIncome_Fail_IncomeRecordNotFound()
        {
            _incomeRepository.Setup(i => i.GetIncomeByIncomeId(It.IsAny<long>()));

            var incomeServices = new BudgetIncomeServices(_incomeRepository.Object);
            Assert.ThrowsAsync<Exception>(() => incomeServices.UpdateIncome(1,2));

            _incomeRepository.Verify(i => i.UpdateIncome(It.IsAny<long>(), It.IsAny<decimal>()), Times.Never);
        }
    }
}
