using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Role
    {
        public Role()
        {
            AccountRoles = new HashSet<AccountRole>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }
}
