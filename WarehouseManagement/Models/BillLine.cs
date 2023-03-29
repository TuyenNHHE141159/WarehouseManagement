using System;
using System.Collections.Generic;

#nullable disable

namespace WarehouseManagement.Models
{
    public partial class BillLine
    {
        public int BillId { get; set; }
        public int FlowerId { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }

        public virtual Bill Bill { get; set; }
        public virtual Flower Flower { get; set; }
    }
}
