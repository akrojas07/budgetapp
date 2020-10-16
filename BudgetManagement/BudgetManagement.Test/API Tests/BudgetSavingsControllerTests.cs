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
using BudgetManagement.API.Models.SavingsModels;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManagement.Test.API_Tests
{
    [TestFixture]
    public class BudgetSavingsControllerTests
    {
        private Mock<IBudgetSavingsServices> _savingsServices;
        
        [SetUp]
        public void Setup()
        {
            _savingsServices = new Mock<IBudgetSavingsServices>();
        }

        [Test]
        public async Task Test_AddNewSavings_Success()
        {
            _savingsServices.Setup(s => s.AddNewSaving(It.IsAny<BudgetSavingsModel>()))
                .Returns(Task.CompletedTask);

            var controller = new BudgetSavingsController(_savingsServices.Object);
            var response = await controller.AddNewSaving(new AddNewSavingRequest() 
            { 
                UserId = 1,
                SavingsType = "Money Market",
                SavingsAmount = 50 
            });

            Assert.NotNull(response);
            Assert.AreEqual(201, ((StatusCodeResult)response).StatusCode);
        }

        [Test]
        public async Task Test_AddNewSavings_Fail_BadRequest()
        {
            var controller = new BudgetSavingsController(_savingsServices.Object);
            var response = await controller.AddNewSaving(null);

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);

        }

        [Test]
        public async Task Test_AddNewSavings_Fail_BadArgument()
        {
            _savingsServices.Setup(s => s.AddNewSaving(It.IsAny<BudgetSavingsModel>()))
                .ThrowsAsync(new ArgumentException("Bad Argument"));

            var controller = new BudgetSavingsController(_savingsServices.Object);
            var response = await controller.AddNewSaving(new AddNewSavingRequest()
            {
                UserId = 1,
                SavingsType = "Money Market",
                SavingsAmount = 50
            });

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_AddNewSavings_Fail_InternalError()
        {
            _savingsServices.Setup(s => s.AddNewSaving(It.IsAny<BudgetSavingsModel>()))
                .ThrowsAsync(new Exception("Internal Error"));

            var controller = new BudgetSavingsController(_savingsServices.Object);
            var response = await controller.AddNewSaving(new AddNewSavingRequest()
            {
                UserId = 1,
                SavingsType = "Money Market",
                SavingsAmount = 50
            });

            Assert.NotNull(response);
            Assert.AreEqual(500, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_GetAllSavingsByUserID_Success()
        {
            _savingsServices.Setup(s => s.GetAllSavingsByUserId(It.IsAny<long>()))
                .ReturnsAsync(new List<BudgetSavingsModel>());

            var controller = new BudgetSavingsController(_savingsServices.Object);
            var response = await controller.GetAllSavingsByUserId(new GetAllSavingsByUserIdRequest() { UserId = 1});

            Assert.NotNull(response);
            Assert.AreEqual(200, ((OkObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_GetAllSavingsByUserId_Fail_BadArgument()
        {
            _savingsServices.Setup(s => s.GetAllSavingsByUserId(It.IsAny<long>()))
                .ThrowsAsync(new ArgumentException("Bad argument"));
            var controller = new BudgetSavingsController(_savingsServices.Object);
            var response = await controller.GetAllSavingsByUserId(new GetAllSavingsByUserIdRequest() { UserId = 1 });

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_GetAllSavingsByUserId_Fail_BadRequest()
        {
            var controller = new BudgetSavingsController(_savingsServices.Object);
            var response = await controller.GetAllSavingsByUserId(null);

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_GetAllSavingsByUserId_Fail_InternalError()
        {
            _savingsServices.Setup(s => s.GetAllSavingsByUserId(It.IsAny<long>()))
                .ThrowsAsync(new Exception("Internal Error"));

            var controller = new BudgetSavingsController(_savingsServices.Object);
            var response = await controller.GetAllSavingsByUserId(new GetAllSavingsByUserIdRequest() { UserId = 1 });

            Assert.NotNull(response);
            Assert.AreEqual(500, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_RemoveSaving_Success()
        {
            _savingsServices.Setup(s => s.RemoveSaving(It.IsAny<long>()))
                .Returns(Task.CompletedTask);

            var controller = new BudgetSavingsController(_savingsServices.Object);
            var response = await controller.RemoveSaving(new RemoveSavingRequest() { SavingsId = 1});

            Assert.NotNull(response);
            Assert.AreEqual(200, ((OkResult)response).StatusCode);

        }

        [Test]
        public async Task Test_RemoveSavings_Fail_BadRequest()
        {
            var controller = new BudgetSavingsController(_savingsServices.Object);
            var response = await controller.RemoveSaving(null);

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_RemoveSavings_Fail_BadArgument()
        {
            _savingsServices.Setup(s => s.RemoveSaving(It.IsAny<long>()))
                .ThrowsAsync(new ArgumentException("Bad Argument")) ;

            var controller = new BudgetSavingsController(_savingsServices.Object);
            var response = await controller.RemoveSaving(new RemoveSavingRequest() { SavingsId = 1 });

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_RemoveSavings_Fail_InternalError()
        {
            _savingsServices.Setup(s => s.RemoveSaving(It.IsAny<long>()))
                .ThrowsAsync(new Exception("Internal Error"));

            var controller = new BudgetSavingsController(_savingsServices.Object);
            var response = await controller.RemoveSaving(new RemoveSavingRequest() { SavingsId = 1 });

            Assert.NotNull(response);
            Assert.AreEqual(500, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_UpdateSaving_Success()
        {
            _savingsServices.Setup(s => s.UpdateSaving(It.IsAny<long>(), It.IsAny<decimal>()))
                .Returns(Task.CompletedTask);

            var controller = new BudgetSavingsController(_savingsServices.Object);
            var response = await controller.UpdateSaving(new UpdateSavingRequest() { SavingsId = 1, SavingsAmount = 5});

            Assert.NotNull(response);
            Assert.AreEqual(200, ((OkResult)response).StatusCode);

        }

        [Test]
        public async Task Test_UpdateSavings_Fail_BadRequest()
        {
            var controller = new BudgetSavingsController(_savingsServices.Object);
            var response = await controller.UpdateSaving(null);

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_UpdateSavings_Fail_BadArgument()
        {
            _savingsServices.Setup(s => s.UpdateSaving(It.IsAny<long>(), It.IsAny<decimal>()))
                .ThrowsAsync(new ArgumentException("Bad Argument"));

            var controller = new BudgetSavingsController(_savingsServices.Object);
            var response = await controller.UpdateSaving(new UpdateSavingRequest() { SavingsId = 1, SavingsAmount = 5 });

            Assert.NotNull(response);
            Assert.AreEqual(400, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_UpdateSavings_Fail_InternalError()
        {
            _savingsServices.Setup(s => s.UpdateSaving(It.IsAny<long>(), It.IsAny<decimal>()))
                .ThrowsAsync(new Exception("Internal Error"));

            var controller = new BudgetSavingsController(_savingsServices.Object);
            var response = await controller.UpdateSaving(new UpdateSavingRequest() { SavingsId = 1 , SavingsAmount= 5});

            Assert.NotNull(response);
            Assert.AreEqual(500, ((ObjectResult)response).StatusCode);
        }
    }
}
