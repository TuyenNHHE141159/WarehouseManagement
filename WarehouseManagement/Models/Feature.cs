using System;
using System.Collections.Generic;

#nullable disable

namespace WarehouseManagement.Models
{
    public partial class Feature
    {
        public Feature()
        {
            FeatureGroups = new HashSet<FeatureGroup>();
        }

        public int Fid { get; set; }
        public string Url { get; set; }

        public virtual ICollection<FeatureGroup> FeatureGroups { get; set; }
    }
}
