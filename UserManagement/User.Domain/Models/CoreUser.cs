using System;
using System.Collections.Generic;
using System.Text;

namespace User.Domain.Models
{
    public class CoreUser
    {
        public CoreUser() { }
        public CoreUser(string email, string firstName, string lastName, string password, bool status)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Status = status;
        }
        public long Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public bool? Status { get; set; }

    }
}
