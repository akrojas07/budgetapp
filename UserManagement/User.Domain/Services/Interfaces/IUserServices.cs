using System.Threading.Tasks;
using User.Domain.Models;

namespace User.Domain.Services.Interfaces
{
    public interface IUserServices
    {
        // ------ Create New User Account ----- \\
        Task<long> CreateNewUserAccount(CoreUser coreUser);


        // --------- pull user object ----------\\ 
        Task<CoreUser> GetUserByEmail(string email);
        Task<CoreUser> GetUserById(long id);


        // --------- update user --------------- \\ 
        Task UpdateUserEmail(long userId, string email);
        Task UpdateUserPassword(long userId, string password);
        Task UpdateName(long userId, string nameType, string name);
        Task<long> LogIn(string userEmail, string password);
        Task LogOut(long userId);
        


        //--------- delete user ----------------- \\
        Task DeleteUserAccount(long userId);
    }
}
