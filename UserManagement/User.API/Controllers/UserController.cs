using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using User.API.Models;
using User.Domain.Services.Interfaces;
using User.Domain.Models;

namespace User.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewUserAccount([FromBody]CreateNewUserAccountRequest createNewUserAccount)
        {
            try
            {
                CoreUser coreUser = new CoreUser()
                {
                    Email = createNewUserAccount.Email,
                    FirstName = createNewUserAccount.FirstName,
                    LastName = createNewUserAccount.LastName,
                    Password = createNewUserAccount.Password,
                    Status = true
                };

                await _userServices.CreateNewUserAccount(coreUser);

                return StatusCode(201);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserAccount([FromBody] DeleteAccountRequest deleteAccount)
        {
            try
            {
                await _userServices.DeleteUserAccount(deleteAccount.UserId);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        
        [HttpGet]              
        public async Task<IActionResult> GetUserByEmail([FromQuery]string email)
        {
            try
            {
                return Ok(await _userServices.GetUserByEmail(email));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("login")]

        public async Task<IActionResult> LogIn([FromBody]LogInAccountRequest login)
        {
            try
            {
                await _userServices.LogIn(login.Email, login.Password);
                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("logout")]
        public async Task<IActionResult> LogOut([FromBody] LogOutAccountRequest logOut)
        {
            try
            {
                await _userServices.LogOut(logOut.UserId);
                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("updateName")]
        public async Task<IActionResult> UpdateUserName([FromBody] UpdateNameAccountRequest updateName)
        {
            try
            {
                await _userServices.UpdateName(updateName.UserId, updateName.NameType, updateName.Name);
                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("updateEmail")]
        public async Task<IActionResult> UpdateUserEmail([FromBody] UpdateUserEmailRequest updateEmail)
        {
            try
            {
                await _userServices.UpdateUserEmail(updateEmail.UserId, updateEmail.Email);
                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("updatePassword")]
        public async Task<IActionResult> UpdateUserPassword([FromBody]UpdateUserPasswordRequest updatePassword)
        {
            try
            {
                await _userServices.UpdateUserPassword(updatePassword.UserId, updatePassword.Password);
                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}