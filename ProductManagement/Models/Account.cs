using System;
using System.Collections.Generic;

#nullable disable

namespace ProductManagement.Models
{
    public partial class Account
    {
        public Account()
        {
            AccountRoles = new HashSet<AccountRole>();
        }

        public string AccountId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }
}
