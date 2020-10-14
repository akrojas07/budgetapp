using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;
using BudgetManagement.Persistence.Repositories.Interfaces;
using System.Threading.Tasks;
using BudgetManagement.Persistence.Repositories.Entities;
using BudgetManagement.Domain.Services;
using BudgetManagement.Domain.Models;

namespace BudgetManagement.Test.Domain_Tests
{
    [TestFixture]
    public class BudgetExpenseServiceTest
    {
        private Mock<IBudgetExpensesRepository> _expenseRepository;

        [SetUp]
        public void Setup()
        {
            _expenseRepository = new Mock<IBudgetExpensesRepository>();
        }

        [Test]
        public async Task Test_AddNewExpense_Success()
        {
            _expenseRepository.Setup(e => e.AddNewExpense(It.IsAny<BudgetExpenses>()))
                .Returns(Task.CompletedTask);

            var expenseService = new BudgetExpensesServices(_expenseRepository.Object);

            await expenseService.AddNewExpense(new BudgetExpensesModel()
            {
                UserId = 1,
                ExpenseAmount = 50,
                ExpenseType = "Groceries"
            });

            _expenseRepository.Verify(e => e.AddNewExpense(It.IsAny<BudgetExpenses>()), Times.Once);
        }

        [Test]
        public void Test_AddNewExpense_Fail() 
        {
            var expenseService = new BudgetExpensesServices(_expenseRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => expenseService.AddNewExpense(null));

            _expenseRepository.Verify(e => e.AddNewExpense(It.IsAny<BudgetExpenses>()), Times.Never);

        }

        [Test]
        public async Task Test_GetExpensesByUserId_Success()
        {
            _expenseRepository.Setup(e => e.GetAllExpensesByUserId(It.IsAny<long>()))
                .ReturnsAsync(new List<BudgetExpenses>());

            var expenseServices = new BudgetExpensesServices(_expenseRepository.Object);

            await expenseServices.GetExpensesByUserId(1);

            _expenseRepository.Verify(e => e.GetAllExpensesByUserId(It.IsAny<long>()), Times.Once);
        }

        [Test]

        public void Test_GetExpensesByUserId_Fail()
        {
            _expenseRepository.Setup(e => e.GetAllExpensesByUserId(It.IsAny<long>()));

            var expenseServices = new BudgetExpensesServices(_expenseRepository.Object);

            Assert.ThrowsAsync<Exception>(() => expenseServices.GetExpensesByUserId(0));

            _expenseRepository.Verify(e => e.GetAllExpensesByUserId(It.IsAny<long>()), Times.Once);

        }

        [Test]
        public async Task Test_RemoveExpense_Success() 
        {
            _expenseRepository.Setup(e => e.GetExpenseByExpenseId(It.IsAny<long>()))
                .ReturnsAsync(new BudgetExpenses());
            _expenseRepository.Setup(e => e.RemoveExpense(It.IsAny<long>()))
                .Returns(Task.CompletedTask);

            var expenseServices = new BudgetExpensesServices(_expenseRepository.Object);

            await expenseServices.RemoveExpense(1);

            _expenseRepository.Verify(e => e.RemoveExpense(It.IsAny<long>()), Times.Once);
        }

        [Test]
        public void Test_RemoveExpense_Fail() 
        {
            _expenseRepository.Setup(e => e.GetExpenseByExpenseId(It.IsAny<long>()));
            _expenseRepository.Setup(e => e.RemoveExpense(It.IsAny<long>()))
                .Throws<Exception>();

            var expenseServices = new BudgetExpensesServices(_expenseRepository.Object);
            Assert.ThrowsAsync<Exception>(() => expenseServices.RemoveExpense(1));

            _expenseRepository.Verify(e => e.RemoveExpense(It.IsAny<long>()), Times.Never);

        }

        [Test]
        public async Task Test_UpdateExpense_Success() 
        {
            _expenseRepository.Setup(e => e.GetExpenseByExpenseId(It.IsAny<long>()))
                .ReturnsAsync(new BudgetExpenses());
            _expenseRepository.Setup(e => e.UpdateExpense(It.IsAny<long>(), It.IsAny<decimal>()))
                .Returns(Task.CompletedTask);

            var expenseServices = new BudgetExpensesServices(_expenseRepository.Object);
            await expenseServices.UpdateExpense(1,4);

            _expenseRepository.Verify(e => e.UpdateExpense(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);

        }

        [Test]
        public void Test_UpdateExpense_Fail() 
        {
            _expenseRepository.Setup(e => e.GetExpenseByExpenseId(It.IsAny<long>()));
            _expenseRepository.Setup(e => e.UpdateExpense(It.IsAny<long>(), It.IsAny<decimal>()));

            var expenseServices = new BudgetExpensesServices(_expenseRepository.Object);
            Assert.ThrowsAsync<Exception>(() => expenseServices.UpdateExpense(1,2));

            _expenseRepository.Verify(e => e.UpdateExpense(It.IsAny<long>(), It.IsAny<decimal>()), Times.Never);
        }

    }
}
