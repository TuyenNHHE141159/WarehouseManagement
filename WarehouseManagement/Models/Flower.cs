using System;
using System.Collections.Generic;

#nullable disable

namespace WarehouseManagement.Models
{
    public partial class Flower
    {
        public Flower()
        {
            BillLines = new HashSet<BillLine>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }

        public virtual ICollection<BillLine> BillLines { get; set; }
    }
}
