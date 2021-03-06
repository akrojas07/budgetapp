﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

using DbUser = User.Infrastructure.Repository.Entities.UserAccount;
using User.Infrastructure.Repository.Entities;

namespace User.Infrastructure.Repository.UserRepositories
{ //Package Manager Console => Scaffold-DbContext "Server=DESKTOP-LQ9NL1I;Database=BudgetApp;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Repository/Entities -f
    public class UserRepository : IUserRepository
    {
        public async Task<long> CreateNewUserAccount(DbUser user)
        {
            using(var context = new BudgetAppContext())
            {
                if (user == null)
                {
                    throw new Exception("User not provided");
                }

                context.UserAccount.Add(user);

                await context.SaveChangesAsync();

                return user.Id;
            }
        }

        public async Task DeleteUserAccount(long userId)
        {
            using(var context = new BudgetAppContext())
            {
                var user = await context.UserAccount.FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                context.UserAccount.Remove(user);

                await context.SaveChangesAsync();

            }
        }

        public async Task<List<DbUser>> GetAllUsers()
        {
            List<DbUser> dbUsers = new List<DbUser>();

            using (var context = new BudgetAppContext())
            {
                dbUsers = await context.UserAccount.ToListAsync();

                if(dbUsers.Count < 1)
                {
                    throw new Exception("No Users found");
                }

            }

            return dbUsers;

        }

        public async Task<DbUser> GetUserByEmail(string email)
        {
            using(var context = new BudgetAppContext())
            {
                var dbUser = await context.UserAccount.FirstOrDefaultAsync(u => u.Email == email);
                
                if (dbUser == null)
                {
                    throw new Exception("User not found");
                }

                return dbUser;
            }
        }

        public async Task<DbUser> GetUserByUserId(long userId)
        {
            using(var context = new BudgetAppContext())
            {
                var dbUser = await context.UserAccount.FirstOrDefaultAsync(u => u.Id == userId);
                
                if (dbUser == null)
                {
                    throw new Exception("User not found");
                }

                return dbUser;
            }

        }

        public async Task<long> UpdateStatus(long userId, bool status)
        {
            using (var context = new BudgetAppContext())
            {
                var dbUser = await context.UserAccount.FirstOrDefaultAsync(u => u.Id == userId);

                if(dbUser == null)
                {
                    throw new Exception("User not found");
                }
                
                dbUser.Status = status;
                dbUser.Updated = DateTime.Now;
                await context.SaveChangesAsync();

                return dbUser.Id;
            }
        }

        public async Task UpdateUserEmail(long userId, string email)
        {
            using (var context = new BudgetAppContext())
            {
                var dbUser = await context.UserAccount.FirstOrDefaultAsync(u => u.Id == userId);

                if(dbUser == null)
                {
                    throw new Exception("User not found");
                }
                
                dbUser.Email = email;
                dbUser.Updated = DateTime.Now;
                await context.SaveChangesAsync();

            }
        }

        public async Task UpdateName(long userId, string firstName, string lastName)
        {
            using (var context = new BudgetAppContext())
            {
                var dbUser = await context.UserAccount.FirstOrDefaultAsync(u => u.Id == userId);

                if(dbUser == null)
                {
                    throw new Exception("User not found");
                }

                dbUser.FirstName = firstName;
                dbUser.LastName = lastName; 

                dbUser.Updated = DateTime.Now;
                await context.SaveChangesAsync();

            }
        }

        public async Task UpdateUserPassword(long userId, byte[] password)
        {
            using (var context = new BudgetAppContext())
            {
                var dbUser = await context.UserAccount.FirstOrDefaultAsync(u => u.Id == userId);

                if(dbUser == null)
                {
                    throw new Exception("User not found");
                }

                dbUser.Password = password;
                dbUser.Updated = DateTime.Now;
                await context.SaveChangesAsync();


            }
        }
    }
}
