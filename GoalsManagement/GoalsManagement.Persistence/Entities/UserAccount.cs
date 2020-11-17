using System;
using System.Collections.Generic;

#nullable disable

namespace GoalsManagement.Persistence.Entities
{
    public partial class UserAccount
    {
        public UserAccount()
        {
            Goals = new HashSet<Goal>();
        }

        public long Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Password { get; set; }
        public bool Status { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual ICollection<Goal> Goals { get; set; }
    }
}
