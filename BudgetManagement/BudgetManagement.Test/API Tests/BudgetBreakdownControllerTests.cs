using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;
using BudgetManagement.Domain.Services.Interfaces;
using System.Threading.Tasks;
using BudgetManagement.Domain.Models;
using BudgetManagement.API.Controllers;
using BudgetManagement.API.Models.BudgetBreakdownModels;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManagement.Test.API_Tests
{
    [TestFixture]
    public class BudgetBreakdownControllerTests
    {
        private Mock<IBudgetBreakdownServices> _breakdownServices;

        [SetUp] 
        public void Setup()
        {
            _breakdownServices = new Mock<IBudgetBreakdownServices>();
        }

        [Test]
        public async Task Test_AddNewBudgetBreakdown_Success()
        {
            _breakdownServices.Setup(b => b.AddNewBudgetBreakdownByUserId(It.IsAny<BudgetBreakdownModel>()))
                .Returns(Task.CompletedTask);

            var controller = new BudgetBreakdownController(_breakdownServices.Object);
            var response = await controller.AddNewBudgetBreakdown(new AddNewBudgetBreakdownRequest() 
            { 
                UserId = 1,
                BudgetType = "zbb",
                ExpensesBreakdown = .5m,
                SavingsBreakdown = .2m
            
            });

            Assert.NotNull(response);
            Assert.AreEqual(201, ((StatusCodeResult)response).StatusCode); 
        }

        [Test]
        public async Task Test_AddNewBudgetBreakdown_Fail_ArgumentException()
        {
            _breakdownServices.Setup(b => b.AddNewBudgetBreakdownByUserId(It.IsAny<BudgetBreakdownModel>()))
                .Throws<ArgumentException>();

            var controller = new BudgetBreakdownController(_breakdownServices.Object);
            var response = await controller.AddNewBudgetBreakdown(null);

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_AddNewBudgetBreakdown_Fail_Exception()
        {
            _breakdownServices.Setup(b => b.AddNewBudgetBreakdownByUserId(It.IsAny<BudgetBreakdownModel>()))
                .Throws<Exception>();

            var controller = new BudgetBreakdownController(_breakdownServices.Object);
            var response = await controller.AddNewBudgetBreakdown(new AddNewBudgetBreakdownRequest()
            {
                UserId = 1,
                BudgetType = "zbb",
                ExpensesBreakdown = .5m,
                SavingsBreakdown = .2m

            });

            Assert.NotNull(response);
            Assert.AreEqual(500, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_GetBudgetBreakdownByUser_Success()
        {
            _breakdownServices.Setup(b => b.GetBudgetBreakdownByUser(It.IsAny<long>()))
                .ReturnsAsync(new BudgetBreakdownModel());

            var controller = new BudgetBreakdownController(_breakdownServices.Object);
            var response = await controller.GetBudgetBreakdownByUserId(new GetBudgetBreakdownByUserIdRequest() { UserId = 1});

            Assert.NotNull(response);
            Assert.AreEqual(200, ((ObjectResult)response).StatusCode);

        }

        [Test]
        public async Task Test_GetBudgetBreakdownByUser_Fail_ArgumentException()
        {
            _breakdownServices.Setup(b => b.GetBudgetBreakdownByUser(It.IsAny<long>()))
                .Throws<ArgumentException>();

            var controller = new BudgetBreakdownController(_breakdownServices.Object);
            var response = await controller.GetBudgetBreakdownByUserId(null);

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_GetBudgetBreakdownByUser_Fail_Exception()
        {
            _breakdownServices.Setup(b => b.GetBudgetBreakdownByUser(It.IsAny<long>()))
                .Throws<Exception>();

            var controller = new BudgetBreakdownController(_breakdownServices.Object);
            var response = await controller.GetBudgetBreakdownByUserId(new GetBudgetBreakdownByUserIdRequest() { UserId = 1 });

            Assert.NotNull(response);
            Assert.AreEqual(500, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_GetBudgetTypeByUserId_Success()
        {
            _breakdownServices.Setup(b => b.GetBudgetTypeByUserId(It.IsAny<long>()))
                .ReturnsAsync("string");
            var controller = new BudgetBreakdownController(_breakdownServices.Object);
            var response = await controller.GetBudgetTypeByUserId(new GetBudgetTypeByUserId() { UserId = 1 });

            Assert.NotNull(response);
            Assert.AreEqual(200, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_GetBudgetTypeByUserId_Fail_ArgumentException()
        {
            _breakdownServices.Setup(b => b.GetBudgetTypeByUserId(It.IsAny<long>()))
                .Throws<ArgumentException>();
            var controller = new BudgetBreakdownController(_breakdownServices.Object);
            var response = await controller.GetBudgetTypeByUserId(null);

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_GetBudgetTypeByUserId_Fail_Exception()
        {
            _breakdownServices.Setup(b => b.GetBudgetTypeByUserId(It.IsAny<long>()))
                .Throws<Exception>();
            var controller = new BudgetBreakdownController(_breakdownServices.Object);
            var response = await controller.GetBudgetTypeByUserId(new GetBudgetTypeByUserId() { UserId = 1 });

            Assert.NotNull(response);
            Assert.AreEqual(500, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_RemoveBudgetBreakdown_Success()
        {
            _breakdownServices.Setup(b => b.RemoveBudgetBreakdownByUserId(It.IsAny<long>()))
                .Returns(Task.CompletedTask);

            var controller = new BudgetBreakdownController(_breakdownServices.Object);
            var response = await controller.RemoveBudgetBreakdownByUserId(new RemoveBudgetBreakdownRequest() { UserId = 1});

            Assert.NotNull(response);
            Assert.AreEqual(200, ((OkResult)response).StatusCode);
        }

        [Test]
        public async Task Test_RemoveBudgetBreakdown_Fail_ArgumentException()
        {
            _breakdownServices.Setup(b => b.RemoveBudgetBreakdownByUserId(It.IsAny<long>()))
                .Throws<ArgumentException>();

            var controller = new BudgetBreakdownController(_breakdownServices.Object);
            var response = await controller.RemoveBudgetBreakdownByUserId(null);

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_RemoveBudgetBreakdown_Fail_Exception()
        {
            _breakdownServices.Setup(b => b.RemoveBudgetBreakdownByUserId(It.IsAny<long>()))
                .Throws<Exception>();

            var controller = new BudgetBreakdownController(_breakdownServices.Object);
            var response = await controller.RemoveBudgetBreakdownByUserId(new RemoveBudgetBreakdownRequest() { UserId = 1 });

            Assert.NotNull(response);
            Assert.AreEqual(500, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_UpdateBudgetBreakdown_Success()
        {
            _breakdownServices.Setup(b => b.UpdateBudgetBreakdownByUserId(It.IsAny<BudgetBreakdownModel>()))
                .Returns(Task.CompletedTask);

            var controller = new BudgetBreakdownController(_breakdownServices.Object);
            var response = await controller.UpdateBudgetBreakdownByUser(new UpdateBudgetBreakdownRequest() { UserId = 1, BudgetType = "zbb", ExpensesBreakdown = .25m, SavingsBreakdown = .35m});

            Assert.NotNull(response);
            Assert.AreEqual(200, ((OkResult)response).StatusCode);
        }

        [Test]
        public async Task Test_UpdateBudgetBreakdown_Fail_ArgumentException()
        {
            _breakdownServices.Setup(b => b.UpdateBudgetBreakdownByUserId(It.IsAny<BudgetBreakdownModel>()))
                .Throws<ArgumentException>();

            var controller = new BudgetBreakdownController(_breakdownServices.Object);
            var response = await controller.UpdateBudgetBreakdownByUser(null);

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_UpdateBudgetBreakdown_Fail_Exception()
        {
            _breakdownServices.Setup(b => b.UpdateBudgetBreakdownByUserId(It.IsAny<BudgetBreakdownModel>()))
                .Throws<Exception>();

            var controller = new BudgetBreakdownController(_breakdownServices.Object);
            var response = await controller.UpdateBudgetBreakdownByUser(new UpdateBudgetBreakdownRequest() { UserId = 1, BudgetType = "zbb", ExpensesBreakdown = .25m, SavingsBreakdown = .35m });

            Assert.NotNull(response);
            Assert.AreEqual(500, ((ObjectResult)response).StatusCode);

        }
    }
}
