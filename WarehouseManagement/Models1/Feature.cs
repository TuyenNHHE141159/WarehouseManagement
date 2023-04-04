using System;
using System.Collections.Generic;

#nullable disable

namespace WarehouseManagement.Models1
{
    public partial class Feature
    {
        public Feature()
        {
            RoleFeatures = new HashSet<RoleFeature>();
        }

        public int Fid { get; set; }
        public string Feature1 { get; set; }

        public virtual ICollection<RoleFeature> RoleFeatures { get; set; }
    }
}
