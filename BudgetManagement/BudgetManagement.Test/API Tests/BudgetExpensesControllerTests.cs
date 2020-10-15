﻿using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;
using BudgetManagement.Domain.Services.Interfaces;
using System.Threading.Tasks;
using BudgetManagement.Domain.Models;
using BudgetManagement.API.Controllers;
using BudgetManagement.API.Models.ExpenseModels;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManagement.Test.API_Tests
{
    [TestFixture]
    public class BudgetExpensesControllerTests
    {
        private Mock<IBudgetExpensesServices> _expensesServices;

        [SetUp]
        public void Setup()
        {
            _expensesServices = new Mock<IBudgetExpensesServices>();
        }

        [Test]
        public async Task Test_AddNewExpense_Success()
        {
            _expensesServices.Setup(e => e.AddNewExpense(It.IsAny<BudgetExpensesModel>()))
                .Returns(Task.CompletedTask);

            var controller = new BudgetExpensesController(_expensesServices.Object);
            var response = await controller.AddNewExpense(new AddNewExpenseRequest()
            {
                UserId = 1,
                ExpenseAmount = 50,
                ExpenseType = "Groceries"
            });

            Assert.NotNull(response);
            /*Assert.AreEqual(response.GetType(), typeof(StatusCodeResult));*/
            Assert.AreEqual(201, ((StatusCodeResult)response).StatusCode);
        }

        [Test]
        public async Task Test_AddNewExpense_Fail_CallingMethod()
        {
            _expensesServices.Setup(e => e.AddNewExpense(It.IsAny<BudgetExpensesModel>()))
                .ThrowsAsync(new Exception("Exception when trying to add new model"));

            var controller = new BudgetExpensesController(_expensesServices.Object);
            var response = await controller.AddNewExpense(new AddNewExpenseRequest()
            {
                UserId = 1,
                ExpenseAmount = 50,
                ExpenseType = "Groceries"
            });

            Assert.NotNull(response);
            Assert.AreEqual(500, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_AddNewExpense_Fail_NullEntry()
        {
            _expensesServices.Setup(e => e.AddNewExpense(It.IsAny<BudgetExpensesModel>()))
                .ThrowsAsync(new ArgumentException("No data entered"));

            var controller = new BudgetExpensesController(_expensesServices.Object);
            var response = await controller.AddNewExpense(new AddNewExpenseRequest());

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_AddNewExpense_Fail_BadArgument()
        {
            var controller = new BudgetExpensesController(_expensesServices.Object);
            var response = await controller.AddNewExpense(null);
            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_GetAllExpensesByUserId_Success()
        {
            _expensesServices.Setup(e => e.GetExpensesByUserId(It.IsAny<long>()))
                .ReturnsAsync(new List<BudgetExpensesModel>());

            var controller = new BudgetExpensesController(_expensesServices.Object);
            var response = await controller.GetExpensesByUserId(new GetAllExpensesByUserIdRequest() { UserId = 1});

            Assert.NotNull(response);
            Assert.AreEqual(200, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_GetAllExpensesByUserId_Fail_BadRequest()
        {
            var controller = new BudgetExpensesController(_expensesServices.Object);
            var response = await controller.GetExpensesByUserId(null);

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_GetAllExpensesByUserId_Fail_NullEntry()
        {
            _expensesServices.Setup(e => e.GetExpensesByUserId(It.IsAny<long>()))
                .ThrowsAsync(new ArgumentException("Null Entry"));

            var controller = new BudgetExpensesController(_expensesServices.Object);
            var response = await controller.GetExpensesByUserId(new GetAllExpensesByUserIdRequest() { UserId = 1});

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_GetAllExpensesByUserId_InternalServerError()
        {
            _expensesServices.Setup(e => e.GetExpensesByUserId(It.IsAny<long>()))
                .ThrowsAsync(new Exception("Exception"));

            var controller = new BudgetExpensesController(_expensesServices.Object);
            var response = await controller.GetExpensesByUserId(new GetAllExpensesByUserIdRequest() { UserId = 1 });

            Assert.NotNull(response);
            Assert.AreEqual(500, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_RemoveExpense_Success()
        {
            _expensesServices.Setup(e => e.RemoveExpense(It.IsAny<long>()))
                .Returns(Task.CompletedTask);

            var controller = new BudgetExpensesController(_expensesServices.Object);
            var response = await controller.RemoveExpense(new RemoveExpenseRequest()
            {
                ExpenseId = 1
            });

            Assert.NotNull(response);
            Assert.AreEqual(200, ((OkResult)response).StatusCode);
        }

        [Test]
        public async Task Test_RemoveExpense_Fail_BadRequest()
        {
            var controller = new BudgetExpensesController(_expensesServices.Object);
            var response = await controller.RemoveExpense(null);

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_RemoveExpense_Fail_InternalServerError()
        {
            _expensesServices.Setup(e => e.RemoveExpense(It.IsAny<long>()))
                .ThrowsAsync(new Exception("Internal Error"));

            var controller = new BudgetExpensesController(_expensesServices.Object);
            var response = await controller.RemoveExpense(new RemoveExpenseRequest()
            {
                ExpenseId = 1
            });


            Assert.NotNull(response);
            Assert.AreEqual(500, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_UpdateExpense_Success()
        {
            _expensesServices.Setup(e => e.UpdateExpense(It.IsAny<long>(), It.IsAny<decimal>()))
                .Returns(Task.CompletedTask);

            var controller = new BudgetExpensesController(_expensesServices.Object);
            var response = await controller.UpdateExpense(new UpdateExpenseRequest()
            {
                ExpenseId= 1,
                ExpenseAmount = 30
            });

            Assert.NotNull(response);
            Assert.AreEqual(200, ((OkResult)response).StatusCode);

        }

        [Test]
        public async Task Test_UpdateExpense_Fail_BadArgument()
        {
            var controller = new BudgetExpensesController(_expensesServices.Object);
            var response = await controller.UpdateExpense(null);

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_UpdateExpense_Fail_InternalError()
        {
            _expensesServices.Setup(e => e.UpdateExpense(It.IsAny<long>(), It.IsAny<decimal>()))
                .ThrowsAsync(new Exception("Internal Error"));

            var controller = new BudgetExpensesController(_expensesServices.Object);
            var response = await controller.UpdateExpense(new UpdateExpenseRequest()
            {
                ExpenseId = 1,
                ExpenseAmount = 30
            });

            Assert.NotNull(response);
            Assert.AreEqual(500, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_UpdateExpense_Fail_BadRequest()
        {
            _expensesServices.Setup(e => e.UpdateExpense(It.IsAny<long>(), It.IsAny<decimal>()))
                .ThrowsAsync(new ArgumentException("Bad Request"));

            var controller = new BudgetExpensesController(_expensesServices.Object);
            var response = await controller.UpdateExpense(new UpdateExpenseRequest()
            {
                ExpenseId = 1,
                ExpenseAmount = 30
            });

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

    }
}
