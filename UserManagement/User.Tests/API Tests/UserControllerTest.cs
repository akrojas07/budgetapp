using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using User.API.Controllers;
using User.API.Models;
using User.Domain.Models;
using User.Domain.Services.Interfaces;

namespace User.Tests.API_Tests
{
    [TestFixture]
    public class UserControllerTest
    {
        private Mock<IUserServices> _userServices;

        [SetUp]
        public void Setup()
        {
            _userServices = new Mock<IUserServices>();
        }

        [Test]
        public async Task Test_CreateNewUserAccount_Success()
        {
            _userServices.Setup(u => u.CreateNewUserAccount(It.IsAny<CoreUser>()))
                .Returns(Task.CompletedTask);

            var controller = new UserController(_userServices.Object);

            var response = await controller.CreateNewUserAccount(new CreateNewUserAccountRequest());

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(StatusCodeResult));
            Assert.AreEqual(((StatusCodeResult)response).StatusCode, 201);
        }

        [Test]
        public async Task Test_CreateNewUserAccount_Fail()
        {
            _userServices.Setup(u => u.CreateNewUserAccount(It.IsAny<CoreUser>()))
                .ThrowsAsync(new Exception());

            var controller = new UserController(_userServices.Object);

            var response = await controller.CreateNewUserAccount(new CreateNewUserAccountRequest());

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));
            Assert.AreEqual(((ObjectResult)response).StatusCode, 500);
        }

        [Test]
        public async Task Test_DeleteUserAccount_Success()
        {
            _userServices.Setup(u => u.DeleteUserAccount(It.IsAny<long>()))
                .Returns(Task.CompletedTask);

            var controller = new UserController(_userServices.Object);
            var response = await controller.DeleteUserAccount(new DeleteAccountRequest());

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(),typeof(OkResult));
            Assert.AreEqual(((StatusCodeResult)response).StatusCode, 200);
        }

        [Test]
        public async Task Test_DeleteUserAccount_Fail()
        {
            _userServices.Setup(u => u.DeleteUserAccount(It.IsAny<long>()))
                .ThrowsAsync(new Exception());

            var controller = new UserController(_userServices.Object);
            var response = await controller.DeleteUserAccount(new DeleteAccountRequest());

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));
            Assert.AreEqual(((ObjectResult)response).StatusCode, 500);
        }
    }
}
