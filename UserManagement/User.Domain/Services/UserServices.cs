using System;
using System.Threading.Tasks;
using User.Domain.Models;
using User.Domain.Services.Interfaces;
using User.Infrastructure.Repository.Interfaces;
using User.Domain.DbMapper;
using User.Infrastructure.Repository.Entities;

namespace User.Domain.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Method to create new user account
        /// Validates user does not exist based on email address 
        /// </summary>
        /// <param name="coreUser"></param>
        /// <returns>Task</returns>
        public async Task CreateNewUserAccount(CoreUser coreUser)
        {
            UserAccount existingUser = null; 

            try
            {
                //pull any user that exists with email address provided
                existingUser = await _userRepository.GetUserByEmail(coreUser.Email);
            }

            catch(Exception) 
            {
                //map from core to db user
                var dbUser = EfUserMapper.CoreToDbUser(coreUser);
                dbUser.Created = DateTime.Now;
                dbUser.Updated = DateTime.Now;
                //create new user account with repository method
                await _userRepository.CreateNewUserAccount(dbUser);

            }
            
            //validate that user is null
            if(existingUser != null)
            {
                throw new Exception("User with associated email exists.");
            }
        }

        /// <summary>
        /// Method to delete user account by User Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Task</returns>
        public async Task DeleteUserAccount(long userId)
        {
            //pull user object
            var existingUser = _userRepository.GetUserByUserId(userId);
            
            //validate user exists
            if(existingUser == null)
            {
                throw new Exception("User does not exist");
            }

            await _userRepository.DeleteUserAccount(userId);
        }

        /// <summary>
        /// Method to pull user object by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Task</returns>
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
        /// <summary>
        /// Method to log user in with email
        /// Validates password
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="password"></param>
        /// <returns>Task</returns>

        public async Task LogIn(string userEmail, string password)
        {
            //pull user object
            var user = await _userRepository.GetUserByEmail(userEmail);

            //validate user exists
            if(user == null)
            {
                throw new Exception("User does not exist");
            }

            //validate password is correct
            while(user.Password != password)
            {
                throw new Exception("Invalid password");
            }

            //log user in
            await _userRepository.UpdateStatus(user.Id, true);

        }
        /// <summary>
        /// Method to log user out
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task LogOut(long userId)
        {
            //pull user object
            var user = await _userRepository.GetUserByUserId(userId);

            //validate user exists
            if (user == null)
            {
                throw new Exception("User does not exist");
            }

            //log user out
            await _userRepository.UpdateStatus(userId, false);
        }

        /// <summary>
        /// Method to update user's name
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="nameType">Refers to either first name or last name</param>
        /// <param name="name"></param>
        /// <returns>Task</returns>
        public async Task UpdateName(long userId, string nameType, string name)
        {
            //pull user object
            var user = await _userRepository.GetUserByUserId(userId);

            //validate user exists
            if(user == null)
            {
                throw new Exception("User does not exist");
            }

            await _userRepository.UpdateName(userId, nameType, name);

        }

        /// <summary>
        /// Method to update user email
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <returns>Task</returns>
        public async Task UpdateUserEmail(long userId, string email)
        {
            // new user instance
            UserAccount userE = null;

            try
            {
                //pull user by email address 
                 userE = await _userRepository.GetUserByEmail(email);
            }
            catch(Exception)
            {
                try
                {
                    //if email address is not associated to existing user, pull user by user id
                    var userI = await _userRepository.GetUserByUserId(userId);
                    await _userRepository.UpdateUserEmail(userI.Id, email);

                }
                catch (Exception)
                {
                    throw new Exception("User User does not exist");
                }

            }

            //validate that userE is null
            // if not null, don't allow the email address update

            if (userE != null)
            {
                throw new Exception("This email is associated to a different account, please try again");
            }
        }
        /// <summary>
        /// Method to update user password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns>Task</returns>

        public async Task UpdateUserPassword(long userId, string password)
        {
            var user = await _userRepository.GetUserByUserId(userId);

            if(user == null)
            {
                throw new Exception("User does not exist");
            }

            await _userRepository.UpdateUserPassword(userId, password);
        }
    }
}
