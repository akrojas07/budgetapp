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
            catch(Exception e)
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
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserByEmail()
        {
            try
            {
                await _userServices.GetUserByEmail();
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        /*
             public async Task<CoreUser> GetUserByEmail(string email)
    {
        //pull user object
        var dbUser = await _userRepository.GetUserByEmail(email);

        //validate user exists
        if(dbUser == null)
        {
            throw new Exception("User does not exist");
        }

        //map db user to core user
        CoreUser coreUser = EfUserMapper.DbToCoreUser(dbUser);

        return coreUser;

    }    
     */

    }
}