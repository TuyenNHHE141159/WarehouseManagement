using System;
using System.Collections.Generic;

#nullable disable

namespace WarehouseManagement.Models
{
    public partial class Account
    {
        public Account()
        {
            AccountRoles = new HashSet<AccountRole>();
            Customers = new HashSet<Customer>();
        }

        public int AccountId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<AccountRole> AccountRoles { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
