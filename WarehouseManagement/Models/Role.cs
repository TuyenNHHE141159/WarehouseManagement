using System;
using System.Collections.Generic;

#nullable disable

namespace WarehouseManagement.Models
{
    public partial class Role
    {
        public Role()
        {
            AccountRoles = new HashSet<AccountRole>();
            RoleFeatures = new HashSet<RoleFeature>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<AccountRole> AccountRoles { get; set; }
        public virtual ICollection<RoleFeature> RoleFeatures { get; set; }
    }
}
