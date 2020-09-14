using System;
using System.Collections.Generic;

namespace User.Infrastructure.Repository.Entities
{
    public partial class UserAccount
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Password { get; set; }
        public bool Status { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
