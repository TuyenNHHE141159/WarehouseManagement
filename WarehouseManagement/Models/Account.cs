using System;
using System.Collections.Generic;

#nullable disable

namespace WarehouseManagement.Models
{
    public partial class Account
    {
        public Account()
        {
            GroupAccounts = new HashSet<GroupAccount>();
        }

        public string Username { get; set; }
        public string Pass { get; set; }
        public int Accid { get; set; }

        public virtual ICollection<GroupAccount> GroupAccounts { get; set; }
    }
}
