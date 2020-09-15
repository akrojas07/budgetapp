using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using User.API.Models;
using User.Domain.Services.Interfaces;
using User.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace User.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IConfiguration _config;

        public UserController(IUserServices userServices, IConfiguration config)
        {
            _userServices = userServices;
            _config = config;
        }

        [HttpPost]
        [AllowAnonymous]
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
        [AllowAnonymous]
        [Route("login")]

        public async Task<IActionResult> LogIn([FromBody]LogInAccountRequest login)
        {
            try
            {
                await _userServices.LogIn(login.Email, login.Password);
                var tokenstring = GenerateJsonWebToken();

                return Ok(new { token = tokenstring});
            }
            catch(ArgumentException)
            {
                return Unauthorized();
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        private string GenerateJsonWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config.GetSection("Jwt:Issuer").Value,
                    _config.GetSection("Jwt:Issuer").Value,
                    null,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                ) ;

            return new JwtSecurityTokenHandler().WriteToken(token);
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