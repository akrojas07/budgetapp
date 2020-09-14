using System;
using System.Collections.Generic;
using System.Text;

namespace User.Domain.Services.Interfaces
{
    public interface IPasswordService
    {
        byte[] CreatePasswordHash(string password);

        bool VerifyPasswordHash(string password, byte[] passwordHash);
    }
}
