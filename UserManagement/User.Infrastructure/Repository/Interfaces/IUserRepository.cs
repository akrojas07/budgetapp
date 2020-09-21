using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Infrastructure.Repository.Entities;

namespace User.Infrastructure.Repository.Interfaces
{
    public interface IUserRepository
    {
        // --------- create new user ----------\\
        Task<long> CreateNewUserAccount(UserAccount user);

        
        // --------- pull user object ----------\\ 
        Task<UserAccount> GetUserByEmail(string email);
        Task<UserAccount> GetUserByUserId(long userId);
        Task<List<UserAccount>> GetAllUsers();


        // --------- update user --------------- \\ remember to update the "updated" date property
        Task UpdateUserEmail(long userId, string email);
        Task UpdateUserPassword(long userId, byte[] password);
        
        //name type is either first name or last name 
        Task UpdateName(long userId, string nameType, string name);
        Task <long> UpdateStatus(long userId, bool status);



        //--------- delete user ----------------- \\
        Task DeleteUserAccount(long userId);

    }
}
