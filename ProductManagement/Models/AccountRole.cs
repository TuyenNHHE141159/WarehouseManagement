using System;
using System.Collections.Generic;

#nullable disable

namespace ProductManagement.Models
{
    public partial class AccountRole
    {
        public string AccountId { get; set; }
        public string RoleId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Role Role { get; set; }
    }
}
