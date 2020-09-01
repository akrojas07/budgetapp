using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Models;

namespace User.Domain.Services.Interfaces
{
    public interface IUserServices
    {
        // ------ Create New User Account ----- \\
        Task CreateNewUserAccount(CoreUser coreUser);


        // --------- pull user object ----------\\ 
        Task<CoreUser> GetUserByEmail(string email);


        // --------- update user --------------- \\ remember to update the "updated" date property
        Task UpdateUserEmail(long userId, string email);
        Task UpdateUserPassword(long userId, string password);
        Task UpdateName(long userId, string nameType, string name);
        Task LogIn(long userId, string password);
        Task LogOut(long userId);
        


        //--------- delete user ----------------- \\
        Task DeleteUserAccount(long userId);
    }
}
