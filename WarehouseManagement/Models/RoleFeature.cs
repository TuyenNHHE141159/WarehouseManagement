using System;
using System.Collections.Generic;

#nullable disable

namespace WarehouseManagement.Models
{
    public partial class RoleFeature
    {
        public int RoleId { get; set; }
        public int FeatureId { get; set; }

        public virtual Feature Feature { get; set; }
        public virtual Role Role { get; set; }
    }
}
