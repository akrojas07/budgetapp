using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private Mock<IConfiguration> _config;

        [SetUp]
        public void Setup()
        {
            _userServices = new Mock<IUserServices>();
            _config = new Mock<IConfiguration>();

            _config.Setup(c => c.GetSection(It.Is<string>(s => s.Equals("Jwt:Key"))).Value)
                .Returns("ewhsacvopturopgnew5tmsh9w9pjvg0a3syz5px9sjbo7cz17g");

            _config.Setup(c => c.GetSection(It.Is<string>(s => s.Equals("Jwt:Issuer"))).Value)
                .Returns("issuer.com");
        }

        [Test]
        public async Task Test_CreateNewUserAccount_Success()
        {
            _userServices.Setup(u => u.CreateNewUserAccount(It.IsAny<CoreUser>()))
                .ReturnsAsync(1);

            var controller = new UserController(_userServices.Object, _config.Object);

            var response = await controller.CreateNewUserAccount(new CreateNewUserAccountRequest());

            Assert.NotNull(response);
            Assert.AreEqual(((ObjectResult)response).StatusCode, 201);
        }

        [Test]
        public async Task Test_CreateNewUserAccount_Fail()
        {
            _userServices.Setup(u => u.CreateNewUserAccount(It.IsAny<CoreUser>()))
                .ThrowsAsync(new Exception());

            var controller = new UserController(_userServices.Object, _config.Object);

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

            var controller = new UserController(_userServices.Object, _config.Object);
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

            var controller = new UserController(_userServices.Object, _config.Object);
            var response = await controller.DeleteUserAccount(new DeleteAccountRequest());

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));
            Assert.AreEqual(((ObjectResult)response).StatusCode, 500);
        }

        [Test]
        public async Task Test_LogIn_Success()
        {
            _userServices.Setup(u => u.LogIn(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(1);

            var controller = new UserController(_userServices.Object, _config.Object);
            var response = await controller.LogIn(new LogInAccountRequest());

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkObjectResult));
            Assert.AreEqual(((OkObjectResult)response).StatusCode, 200);
        }

        [Test]
        public async Task Test_LogIn_Fail()
        {
            _userServices.Setup(u => u.LogIn(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var controller = new UserController(_userServices.Object, _config.Object);
            var response = await controller.LogIn(new LogInAccountRequest());

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));
            Assert.AreEqual(((ObjectResult)response).StatusCode, 500);
            
        }

        [Test]
        public async Task Test_LogOut_Success()
        {
         
            _userServices.Setup(u => u.LogOut(It.IsAny<long>()))
                .Returns(Task.CompletedTask);

            var controller = new UserController(_userServices.Object, _config.Object);
            var response = await controller.LogOut(new LogOutAccountRequest());

            Assert.NotNull(response);
            Assert.AreEqual(((StatusCodeResult)response).StatusCode, 200);

        }

        [Test]
        public async Task Test_LogOut_Fail()
        {
            _userServices.Setup(u => u.LogOut(It.IsAny<long>()))
                .ThrowsAsync(new Exception());

            var controller = new UserController(_userServices.Object, _config.Object);
            var response = await controller.LogOut(new LogOutAccountRequest());

            Assert.NotNull(response);
            Assert.AreEqual(((ObjectResult)response).StatusCode, 500);
        }

        [Test]
        public async Task Test_UpdateName_Success()
        {
            _userServices.Setup(u => u.UpdateName(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var controller = new UserController(_userServices.Object, _config.Object);
            var response = await controller.UpdateUserName(new UpdateNameAccountRequest());

            Assert.NotNull(response);
            Assert.AreEqual(200, ((StatusCodeResult)response).StatusCode);
        }

        [Test]
        public async Task Test_UpdateName_Fail()
        { 
            _userServices.Setup(u => u.UpdateName(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var controller = new UserController(_userServices.Object, _config.Object);
            var response = await controller.UpdateUserName(new UpdateNameAccountRequest());

            Assert.NotNull(response);
            Assert.AreEqual(500, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_GetUserByEmail_Success()
        {
            _userServices.Setup(u => u.GetUserByEmail(It.IsAny<string>()));

            var controller = new UserController(_userServices.Object, _config.Object);
            var response = await controller.GetUserByEmail("email");

            Assert.NotNull(response);
            Assert.AreEqual(200, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_GetUserByEmail_Fail()
        {
            _userServices.Setup(u => u.GetUserByEmail(It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var controller = new UserController(_userServices.Object, _config.Object);
            var response = await controller.GetUserByEmail("email");

            Assert.NotNull(response);
            Assert.AreEqual(500, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_UpdateUserEmail_Success()
        {
            _userServices.Setup(u => u.UpdateUserEmail(It.IsAny<long>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var controller = new UserController(_userServices.Object, _config.Object);
            var response = await controller.UpdateUserEmail(new UpdateUserEmailRequest());

            Assert.NotNull(response);
            Assert.AreEqual(200, ((StatusCodeResult)response).StatusCode);

        }

        [Test]
        public async Task Test_UpdateUserEmail_Fail()
        {
            _userServices.Setup(u => u.UpdateUserEmail(It.IsAny<long>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var controller = new UserController(_userServices.Object, _config.Object);
            var response = await controller.UpdateUserEmail(new UpdateUserEmailRequest());

            Assert.NotNull(response);
            Assert.AreEqual(500, ((ObjectResult)response).StatusCode);
        }

        [Test]
        public async Task Test_UpdateUserPassword_Success()
        {
            _userServices.Setup(u => u.UpdateUserPassword(It.IsAny<long>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var controller = new UserController(_userServices.Object, _config.Object);
            var response = await controller.UpdateUserPassword(new UpdateUserPasswordRequest());

            Assert.NotNull(response);
            Assert.AreEqual(200, ((StatusCodeResult)response).StatusCode);
        }

        [Test]
        public async Task Test_UpdateUserPassword_Fail()
        {
            _userServices.Setup(u => u.UpdateUserPassword(It.IsAny<long>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var controller = new UserController(_userServices.Object, _config.Object);
            var response = await controller.UpdateUserPassword(new UpdateUserPasswordRequest());

            Assert.NotNull(response);
            Assert.AreEqual(500, ((ObjectResult)response).StatusCode);
        }
    }
}
