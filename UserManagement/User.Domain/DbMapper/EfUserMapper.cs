﻿using DomainUser = User.Domain.Models.CoreUser;
using DbUser = User.Infrastructure.Repository.Entities.UserAccount;
using User.Domain.Services;

namespace User.Domain.DbMapper
{ 
    public static class EfUserMapper
    {
        public static DomainUser DbToCoreUser(this DbUser dbUser)
        {
            DomainUser user = new DomainUser()
            {
                Id = dbUser.Id,
                Email = dbUser.Email,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
                Status = dbUser.Status
                
            };

            return user;

        }

        public static DbUser CoreToDbUser(this DomainUser domainUser)
        {
            DbUser dbUser = new DbUser()
            {
                Id = domainUser.Id,
                Email = domainUser.Email,
                FirstName = domainUser.FirstName,
                LastName = domainUser.LastName,
                Status = domainUser.Status
            };

            return dbUser;
        }
    }
}
