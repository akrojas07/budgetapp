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

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("test");
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewUserAccount([FromBody]CreateNewUserAccountRequest createNewUserAccount)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest("");
            //}

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
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserAccount([FromBody] DeleteAccountRequest deleteAccount)
        {
            //if(!ModelState.IsValid)
            //{
            //    return new HttpResponseMessage(HttpStatusCode.BadRequest);
            //}

            try
            {
                await _userServices.DeleteUserAccount(deleteAccount.UserId);

                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}