using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using User.Domain.Models;
using User.Domain.Services;
using User.Infrastructure.Repository.Entities;
using User.Infrastructure.Repository.Interfaces;
using User.Infrastructure.Repository.UserRepositories;

namespace User.Tests.Domain_Tests
{
    [TestFixture]
    public class UserServicesTest
    {
        private Mock<IUserRepository> _userRepository;

        private byte[] _hashPassword;
        
        [SetUp]
        public void Setup()
        {
            _userRepository = new Mock<IUserRepository>();

            var passwordService = new PasswordService();
            _hashPassword = passwordService.CreatePasswordHash("password");
        }

        [Test]
        public async Task Test_CreateNewUserAccount_Success()
        {
            _userRepository.Setup(u => u.GetUserByEmail(It.IsAny<string>()))
                .Throws<Exception>();

            _userRepository.Setup(u => u.CreateNewUserAccount(It.IsAny<UserAccount>()))
                .ReturnsAsync(1);

            var userService = new UserServices(_userRepository.Object, new PasswordService());

            await userService.CreateNewUserAccount(new CoreUser("Email@Email.com", "FirstName", "LastName", "Password", true));

            _userRepository.Verify(u => u.CreateNewUserAccount(It.IsAny<UserAccount>()), Times.Once);
        }

        [Test]
        public void Test_CreateNewUserAccount_Fail_ExistingUser()
        {
            _userRepository.Setup(u => u.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(new UserAccount());

            _userRepository.Setup(u => u.CreateNewUserAccount(It.IsAny<UserAccount>()));

            var userService = new UserServices(_userRepository.Object, new PasswordService());

            Assert.ThrowsAsync<Exception>(() => userService.CreateNewUserAccount(new CoreUser("Email@Email.com", "FirstName", "LastName", "Password", true)));

            _userRepository.Verify(u => u.CreateNewUserAccount(It.IsAny<UserAccount>()), Times.Never);
        }

        [Test]
        public async Task Test_DeleteUserAccount_Success()
        {
            _userRepository.Setup(u => u.GetUserByUserId(It.IsAny<long>())).ReturnsAsync(new UserAccount());
            _userRepository.Setup(u => u.DeleteUserAccount(It.IsAny<long>()));

            var userService = new UserServices(_userRepository.Object, new PasswordService());

            await userService.DeleteUserAccount(1);

            _userRepository.Verify(u => u.DeleteUserAccount(It.IsAny<long>()), Times.Once);
        }

        [Test]
        public void Test_DeleteUserAccount_Fail_UserNotFound()
        {
            _userRepository.Setup(u => u.GetUserByUserId(It.IsAny<long>())).Throws<Exception>();
            _userRepository.Setup(u => u.DeleteUserAccount(It.IsAny<long>()));

            var userService = new UserServices(_userRepository.Object, new PasswordService());

            Assert.ThrowsAsync<Exception>(() => userService.DeleteUserAccount(1));

            _userRepository.Verify(u => u.DeleteUserAccount(It.IsAny<long>()), Times.Never);
        }

        [Test]
        public async Task Test_GetUserByEmail_Success()
        {
            _userRepository.Setup(u => u.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(new UserAccount());

            var userService = new UserServices(_userRepository.Object, new PasswordService());

            await userService.GetUserByEmail("email");

            _userRepository.Verify(u => u.GetUserByEmail(It.IsAny<string>()), Times.Once);

        }

        [Test]
        public void Test_GetUserByEmail_Fail_EmailNotFound()
        {
            _userRepository.Setup(u => u.GetUserByEmail(It.IsAny<string>())).Throws<Exception>();
            var userService = new UserServices(_userRepository.Object, new PasswordService());
            Assert.ThrowsAsync<Exception>(() => userService.GetUserByEmail("email"));

            _userRepository.Verify(u => u.GetUserByEmail(It.IsAny<string>()), Times.Once);

        }


        [Test]
        public async Task Test_LogIn_Success()
        {
            _userRepository.Setup(u => u.GetUserByEmail(It.IsAny<string>()))
                .ReturnsAsync(new UserAccount() { Email = "email", Password = _hashPassword });
            _userRepository.Setup(u => u.UpdateStatus(It.IsAny<long>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var userService = new UserServices(_userRepository.Object, new PasswordService());

            await userService.LogIn("email", "password");

            _userRepository.Verify(u => u.UpdateStatus(It.IsAny<long>(), It.IsAny<bool>()), Times.Once);
        }

        
        [Test]
        public void Test_LogIn_Fail_IncorrectPassword()
        {
            _userRepository.Setup(u => u.GetUserByEmail(It.IsAny<string>()))
                .ReturnsAsync(new UserAccount() { Email = "email", Password = _hashPassword });
            _userRepository.Setup(u => u.UpdateStatus(It.IsAny<long>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var userService = new UserServices(_userRepository.Object, new PasswordService());

            Assert.ThrowsAsync<ArgumentException>(() => userService.LogIn("email", "password2"));
            _userRepository.Verify(u => u.UpdateStatus(It.IsAny<long>(), It.IsAny<bool>()), Times.Never);

        } 

        [Test]
        public async Task Test_LogOut_Success()
        {
            _userRepository.Setup(u => u.GetUserByUserId(It.IsAny<long>()))
                .ReturnsAsync(new UserAccount());
            _userRepository.Setup(u => u.UpdateStatus(It.IsAny<long>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var userService = new UserServices(_userRepository.Object, new PasswordService());
            await userService.LogOut(1);
            _userRepository.Verify(u => u.UpdateStatus(It.IsAny<long>(), It.IsAny<bool>()), Times.Once);
        }

        [Test]
        public void Test_LogOut_Fail()
        {
            _userRepository.Setup(u => u.GetUserByUserId(It.IsAny<long>()))
                .Throws<Exception>();
            _userRepository.Setup(u => u.UpdateStatus(It.IsAny<long>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            var userService = new UserServices(_userRepository.Object, new PasswordService());
            Assert.ThrowsAsync<Exception>(() => userService.LogOut(1));
            _userRepository.Verify(u => u.UpdateStatus(It.IsAny<long>(), It.IsAny<bool>()), Times.Never);
        }

        [Test]
        public async Task Test_UpdateName_Success()
        {
            _userRepository.Setup(u => u.GetUserByUserId(It.IsAny<long>()))
                .ReturnsAsync(new UserAccount());
            _userRepository.Setup(u => u.UpdateName(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var service = new UserServices(_userRepository.Object, new PasswordService());
            await service.UpdateName(1, "first", "name");
            _userRepository.Verify(u => u.UpdateName(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void Test_UpdateName_Fail()
        {
            _userRepository.Setup(u => u.GetUserByUserId(It.IsAny<long>())).Throws<Exception>();
            _userRepository.Setup(u => u.UpdateName(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var service = new UserServices(_userRepository.Object, new PasswordService());
            Assert.ThrowsAsync<Exception>(() => service.UpdateName(0, "first", "name"));
            _userRepository.Verify(u => u.UpdateName(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task Test_UpdateUserEmail_Success()
        {
            _userRepository.Setup(u => u.GetUserByEmail(It.IsAny<string>()))
                .ThrowsAsync(new Exception());
            _userRepository.Setup(u => u.GetUserByUserId(It.IsAny<long>()))
                .ReturnsAsync(new UserAccount());
            _userRepository.Setup(u => u.UpdateUserEmail(It.IsAny<long>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var service = new UserServices(_userRepository.Object, new PasswordService());
            await service.UpdateUserEmail(1, "emailAK");
            _userRepository.Verify(u => u.UpdateUserEmail(It.IsAny<long>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void Test_UpdateUserEmail_Fail_UserNotFound()
        {
            _userRepository.Setup(u => u.GetUserByEmail(It.IsAny<string>()))
                .ThrowsAsync(new Exception());
            _userRepository.Setup(u => u.GetUserByUserId(It.IsAny<long>()))
                .Throws<Exception>();
            _userRepository.Setup(u => u.UpdateUserEmail(It.IsAny<long>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var service = new UserServices(_userRepository.Object, new PasswordService());
            Assert.ThrowsAsync<Exception>(() => service.UpdateUserEmail(0, "email"));
            _userRepository.Verify(u => u.UpdateUserEmail(It.IsAny<long>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void Test_UpdateUserEmail_ExistingEmail()
        {
            _userRepository.Setup(u => u.GetUserByEmail(It.IsAny<string>()))
                .ReturnsAsync(new UserAccount());
            _userRepository.Setup(u => u.GetUserByUserId(It.IsAny<long>()))
                .ReturnsAsync(new UserAccount());
            _userRepository.Setup(u => u.UpdateUserEmail(It.IsAny<long>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var userService = new UserServices(_userRepository.Object, new PasswordService());
            Assert.ThrowsAsync<Exception>(() => userService.UpdateUserEmail(1, "email"));
            _userRepository.Verify(u => u.UpdateUserEmail(It.IsAny<long>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task Test_UpdateUserPassword_Success()
        {
            _userRepository.Setup(u => u.GetUserByUserId(It.IsAny<long>())).ReturnsAsync(new UserAccount());
            _userRepository.Setup(u => u.UpdateUserPassword(It.IsAny<long>(), It.IsAny<byte[]>()));

            var service = new UserServices(_userRepository.Object, new PasswordService());
            await service.UpdateUserPassword(1, "password");
            _userRepository.Verify(u => u.UpdateUserPassword(It.IsAny<long>(), It.IsAny<byte[]>()), Times.Once);
        }

        [Test]
        public void Test_UpdateUserPassword_Fail()
        {
            _userRepository.Setup(u => u.GetUserByUserId(It.IsAny<long>())).Throws<Exception>();
            _userRepository.Setup(u => u.UpdateUserPassword(It.IsAny<long>(), It.IsAny<byte[]>()));

            var service = new UserServices(_userRepository.Object, new PasswordService());
            Assert.ThrowsAsync<Exception>(() => service.UpdateUserPassword(0, "password"));
            _userRepository.Verify(u => u.UpdateUserPassword(It.IsAny<long>(), It.IsAny<byte[]>()), Times.Never);
        }
    }
}
