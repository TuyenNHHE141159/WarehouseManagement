using System;
using System.Collections.Generic;

#nullable disable

namespace ProductManagement.Models
{
    public partial class BillType
    {
        public BillType()
        {
            Bills = new HashSet<Bill>();
        }

        public string Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}
