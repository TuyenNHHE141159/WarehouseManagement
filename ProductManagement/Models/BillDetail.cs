using System;
using System.Collections.Generic;

#nullable disable

namespace ProductManagement.Models
{
    public partial class BillDetail
    {
        public string BillDetailsId { get; set; }
        public string BillId { get; set; }
        public string ProductId { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }

        public virtual Bill Bill { get; set; }
    }
}
