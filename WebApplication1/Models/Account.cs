using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Account
    {
        public Account()
        {
            AccountRoles = new HashSet<AccountRole>();
        }

        public int AccountId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }
}
