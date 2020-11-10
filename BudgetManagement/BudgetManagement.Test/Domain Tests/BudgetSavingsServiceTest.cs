using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;
using BudgetManagement.Domain.Services.Interfaces;
using System.Threading.Tasks;
using BudgetManagement.Persistence.Repositories.Entities;
using BudgetManagement.Domain.Models;
using BudgetManagement.Domain.Services;
using BudgetManagement.Persistence.Repositories.Interfaces;

namespace BudgetManagement.Test.Domain_Tests
{
    [TestFixture]
    public class BudgetSavingsServiceTest
    {
        private  Mock<IBudgetSavingsRepository> _savingsRepository;
        
        [SetUp]
        public void Setup()
        {
            _savingsRepository = new Mock<IBudgetSavingsRepository>();
        }

        [Test]
        public async Task Test_UpsertSavings_Success()
        {
            _savingsRepository.Setup(s => s.UpsertSavings(It.IsAny<List<BudgetSavings>>()))
                .Returns(Task.CompletedTask);

            var savingsServices = new BudgetSavingsServices(_savingsRepository.Object);
            await savingsServices.UpsertSavings(new List<BudgetSavingsModel>() 
            { 
                new BudgetSavingsModel{UserId = 2, SavingsAmount = 500, SavingsType="Money Market"},
                new BudgetSavingsModel{Id = 1, UserId = 4, SavingsType = "Money Market", SavingsAmount = 200}
            });

            _savingsRepository.Verify(s => s.UpsertSavings(It.IsAny<List<BudgetSavings>>()), Times.Once);
        }

        [Test]
        public void Test_UpsertSavings_Fail_EmptyList()
        {
            _savingsRepository.Setup(s => s.UpsertSavings(It.IsAny<List<BudgetSavings>>()))
                .Returns(Task.CompletedTask);

            var savingsServices = new BudgetSavingsServices(_savingsRepository.Object);
            Assert.ThrowsAsync<ArgumentException>(() => savingsServices.UpsertSavings(new List<BudgetSavingsModel>()));

            _savingsRepository.Verify(s => s.UpsertSavings(It.IsAny<List<BudgetSavings>>()), Times.Never);
        }

        [Test]
        public void Test_UpsertSavings_Fail_NullEntry()
        {
            var savingsServices = new BudgetSavingsServices(_savingsRepository.Object);
            Assert.ThrowsAsync<NullReferenceException>(() => savingsServices.UpsertSavings(null));

            _savingsRepository.Verify(s => s.UpsertSavings(It.IsAny<List<BudgetSavings>>()), Times.Never);
        }

        [Test]
        public async Task Test_AddNewSaving_Success()
        {
            _savingsRepository.Setup(s => s.AddNewSaving(It.IsAny<BudgetSavings>()))
                .Returns(Task.CompletedTask);

            var savingsServices = new BudgetSavingsServices(_savingsRepository.Object);
            await savingsServices.AddNewSaving(new BudgetSavingsModel()
            {
                UserId = 1,
                SavingsAmount= 5,
                SavingsType = "Money Market Account"
            });

            _savingsRepository.Verify(s => s.AddNewSaving(It.IsAny<BudgetSavings>()), Times.Once);


        }

        [Test]
        public void Test_AddNewSaving_Fail()
        {
            var savingsServices = new BudgetSavingsServices(_savingsRepository.Object);
            Assert.ThrowsAsync<ArgumentException>(() => savingsServices.AddNewSaving(null));

            _savingsRepository.Verify(s => s.AddNewSaving(It.IsAny<BudgetSavings>()), Times.Never);
        }

        [Test]
        public async Task Test_GetAllSavingsByUserId_Success()
        {
            _savingsRepository.Setup(s => s.GetAllSavingsByUserId(It.IsAny<long>()))
                .ReturnsAsync(new List<BudgetSavings>());

            var savingsServices = new BudgetSavingsServices(_savingsRepository.Object);
            await savingsServices.GetAllSavingsByUserId(1);

            _savingsRepository.Verify(s => s.GetAllSavingsByUserId(It.IsAny<long>()), Times.Once);
        }

        [Test]
        public void Test_GetAllSavingsByUserId_Fail()
        {
            _savingsRepository.Setup(s => s.GetAllSavingsByUserId(It.IsAny<long>()));

            var savingsServices = new BudgetSavingsServices(_savingsRepository.Object);
            Assert.ThrowsAsync<Exception>(() => savingsServices.GetAllSavingsByUserId(1));

            _savingsRepository.Verify(s => s.GetAllSavingsByUserId(It.IsAny<long>()), Times.Once);
        }

        [Test]
        public async Task Test_RemoveSaving_Success()
        {
            _savingsRepository.Setup(s => s.GetSavingBySavingId(It.IsAny<long>()))
                .ReturnsAsync(new BudgetSavings());
            _savingsRepository.Setup(s => s.RemoveSaving(It.IsAny<long>()))
                .Returns(Task.CompletedTask);

            var savingsServices = new BudgetSavingsServices(_savingsRepository.Object);

            await savingsServices.RemoveSaving(1);

            _savingsRepository.Verify(s => s.RemoveSaving(It.IsAny<long>()), Times.Once);
        }

        [Test]
        public void Test_RemoveSaving_Fail()
        {
            _savingsRepository.Setup(s => s.GetSavingBySavingId(It.IsAny<long>()));

            var savingsServices = new BudgetSavingsServices(_savingsRepository.Object);
            Assert.ThrowsAsync<Exception>(() => savingsServices.RemoveSaving(1));

            _savingsRepository.Verify(s => s.RemoveSaving(It.IsAny<long>()), Times.Never);

        }

        [Test]
        public async Task Test_UpdateSaving_Success() 
        {
            _savingsRepository.Setup(s => s.GetSavingBySavingId(It.IsAny<long>()))
                .ReturnsAsync(new BudgetSavings());
            _savingsRepository.Setup(s => s.UpdateSaving(It.IsAny<long>(), It.IsAny<decimal>()))
                .Returns(Task.CompletedTask);

            var savingsServices = new BudgetSavingsServices(_savingsRepository.Object);

            await savingsServices.UpdateSaving(1,1);

            _savingsRepository.Verify(s => s.UpdateSaving(It.IsAny<long>(), It.IsAny<decimal>()),Times.Once);

        }

        [Test]
        public void Test_UpdateSaving_Fail()
        {
            _savingsRepository.Setup(s => s.GetSavingBySavingId(It.IsAny<long>()));

            var savingsServices = new BudgetSavingsServices(_savingsRepository.Object);

            Assert.ThrowsAsync<Exception>(() => savingsServices.UpdateSaving(1,2));

            _savingsRepository.Verify(s => s.UpdateSaving(It.IsAny<long>(), It.IsAny<decimal>()), Times.Never);

        }

    }
}
