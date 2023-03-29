using System;
using System.Collections.Generic;

#nullable disable

namespace WarehouseManagement.Models
{
    public partial class Group
    {
        public Group()
        {
            FeatureGroups = new HashSet<FeatureGroup>();
            GroupAccounts = new HashSet<GroupAccount>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FeatureGroup> FeatureGroups { get; set; }
        public virtual ICollection<GroupAccount> GroupAccounts { get; set; }
    }
}
