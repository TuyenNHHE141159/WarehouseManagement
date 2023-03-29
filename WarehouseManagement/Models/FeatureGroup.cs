using System;
using System.Collections.Generic;

#nullable disable

namespace WarehouseManagement.Models
{
    public partial class FeatureGroup
    {
        public int Fid { get; set; }
        public int Gid { get; set; }

        public virtual Feature FidNavigation { get; set; }
        public virtual Group GidNavigation { get; set; }
    }
}
