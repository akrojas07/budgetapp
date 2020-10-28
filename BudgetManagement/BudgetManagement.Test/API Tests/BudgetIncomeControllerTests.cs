using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;
using System.Linq;
using BudgetManagement.Domain.Services.Interfaces;
using System.Threading.Tasks;
using BudgetManagement.Domain.Models;
using BudgetManagement.API.Controllers;
using BudgetManagement.API.Models.IncomeModels;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManagement.Test.API_Tests
{
    [TestFixture]
    public class BudgetIncomeControllerTests
    {
        private Mock<IBudgetIncomeServices> _incomeServices;

        [SetUp]
        public void Setup()
        {
            _incomeServices = new Mock<IBudgetIncomeServices>();
        }

        [Test]
        public async Task Test_AddNewIncome_Success()
        {
            _incomeServices.Setup(i => i.AddNewIncome(It.IsAny<BudgetIncomeModel>()))
                .Returns(Task.CompletedTask);

            var controller = new BudgetIncomeController(_incomeServices.Object);
            var response = await controller.AddNewIncome(new AddNewIncomeRequest() 
            { 
                UserId = 1,
                IncomeAmount = 50,
                IncomeType = "Paycheck"
            });

            Assert.NotNull(response);
            Assert.AreEqual(201, ((StatusCodeResult)response).StatusCode);
        }

        [Test]
        public async Task Test_AddNewIncome_Fail_IncomeNotPassedToController()
        {
            var controller = new BudgetIncomeController(_incomeServices.Object);
            var response = await controller.AddNewIncome(null);

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_AddNewIncome_Fail_IncomeNotPassedToService()
        {
            _incomeServices.Setup(i => i.AddNewIncome(It.IsAny<BudgetIncomeModel>()))
                .ThrowsAsync(new ArgumentException("Income details not provided"));

            var controller = new BudgetIncomeController(_incomeServices.Object);
            var response = await controller.AddNewIncome(new AddNewIncomeRequest() 
            { 
                UserId = 1,
                IncomeAmount = 34.5m,
                IncomeType = "Paycheck"
            });

            Assert.NotNull(response);
            Assert.AreEqual(400,((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_AddNewIncome_Fail_InternalServerError()
        {
            _incomeServices.Setup(i => i.AddNewIncome(It.IsAny<BudgetIncomeModel>()))
                .ThrowsAsync(new Exception("Internal Error"));

            var controller = new BudgetIncomeController(_incomeServices.Object);
            var response = await controller.AddNewIncome(new AddNewIncomeRequest());

            Assert.NotNull(response);
            Assert.AreEqual(500, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_GetAllIncomeByUserId_Success()
        {
            _incomeServices.Setup(i => i.GetAllIncomeByUserId(It.IsAny<long>()))
                .ReturnsAsync(new List<BudgetIncomeModel>());

            var controller = new BudgetIncomeController(_incomeServices.Object);
            var response = await controller.GetAllIncomeByUserId(1);

            Assert.NotNull(response);
            Assert.AreEqual(200, ((OkObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_GetAllIncomeByUserId_Fail_BadRequest()
        {
            _incomeServices.Setup(i => i.GetAllIncomeByUserId(It.IsAny<long>()))
                .ThrowsAsync(new ArgumentException("Bad Request"));

            var controller = new BudgetIncomeController(_incomeServices.Object);
            var response = await controller.GetAllIncomeByUserId(1);

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_GetAllIncomeByUserID_Fail_BadArgument()
        {
            var controller = new BudgetIncomeController(_incomeServices.Object);
            var response = await controller.GetAllIncomeByUserId(0);

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_GetAllIncomeByUserId_Fail_InternalError()
        {
            _incomeServices.Setup(i => i.GetAllIncomeByUserId(It.IsAny<long>()))
                .ThrowsAsync(new Exception("Internal Error"));

            var controller = new BudgetIncomeController(_incomeServices.Object);
            var response = await controller.GetAllIncomeByUserId(1);

            Assert.NotNull(response);
            Assert.AreEqual(500, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_RemoveIncome_Success()
        {
            _incomeServices.Setup(i => i.RemoveIncome(It.IsAny<long>()))
                .Returns(Task.CompletedTask);

            var controller = new BudgetIncomeController(_incomeServices.Object);
            var response = await controller.RemoveIncome(new RemoveIncomeRequest() { IncomeId = 1 });

            Assert.NotNull(response);
            Assert.AreEqual(200, ((OkResult)response).StatusCode);
        }

        [Test]
        public async Task Test_RemoveIncome_Fail_BadArgument()
        {
            var controller = new BudgetIncomeController(_incomeServices.Object);
            var response = await controller.RemoveIncome(null);

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_RemoveIncome_Fail_BadRequest()
        {
            _incomeServices.Setup(i => i.RemoveIncome(It.IsAny<long>()))
                .ThrowsAsync(new ArgumentException("Bad Request"));

            var controller = new BudgetIncomeController(_incomeServices.Object);
            var response = await controller.RemoveIncome(new RemoveIncomeRequest() { IncomeId = 1 });

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_RemoveIncome_Fail_InternalError()
        {
            _incomeServices.Setup(i => i.RemoveIncome(It.IsAny<long>()))
                .ThrowsAsync(new Exception("Internal Error"));

            var controller = new BudgetIncomeController(_incomeServices.Object);
            var response = await controller.RemoveIncome(new RemoveIncomeRequest() { IncomeId = 1 });

            Assert.NotNull(response);
            Assert.AreEqual(500, ((ObjectResult)response).StatusCode);
        }
        [Test]
        public async Task Test_UpdateIncome_Success()
        {
            _incomeServices.Setup(i => i.UpdateIncome(It.IsAny<long>(), It.IsAny<decimal>()))
                .Returns(Task.CompletedTask);

            var controller = new BudgetIncomeController(_incomeServices.Object);
            var response = await controller.UpdateIncome(new UpdateIncomeRequest() 
            { 
                IncomeAmount = 5,
                IncomeId = 5
            });

            Assert.NotNull(response);
            Assert.AreEqual(200, ((OkResult)response).StatusCode);
        }

        [Test]
        public async Task Test_UpdateIncome_Fail_BadArgument()
        {
            var controller = new BudgetIncomeController(_incomeServices.Object);
            var response = await controller.UpdateIncome(null);

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_UpdateIncome_Fail_BadRequest()
        {
            _incomeServices.Setup(i => i.UpdateIncome(It.IsAny<long>(), It.IsAny<decimal>()))
                .ThrowsAsync(new ArgumentException("Bad Request"));

            var controller = new BudgetIncomeController(_incomeServices.Object);
            var response = await controller.UpdateIncome(new UpdateIncomeRequest()
            {
                IncomeAmount = 5,
                IncomeId = 5
            });

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_UpdateIncome_Fail_InternalServerError()
        {
            _incomeServices.Setup(i => i.UpdateIncome(It.IsAny<long>(), It.IsAny<decimal>()))
                .ThrowsAsync(new Exception("Internal Error"));

            var controller = new BudgetIncomeController(_incomeServices.Object);
            var response = await controller.UpdateIncome(new UpdateIncomeRequest()
            {
                IncomeAmount = 5,
                IncomeId = 5
            });

            Assert.NotNull(response);
            Assert.AreEqual(500, ((ObjectResult)response).StatusCode);
        }

    }
}
