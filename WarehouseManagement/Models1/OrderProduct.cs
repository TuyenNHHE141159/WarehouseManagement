using System;
using System.Collections.Generic;

#nullable disable

namespace WarehouseManagement.Models1
{
    public partial class OrderProduct
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
