using System;
using System.Collections.Generic;

#nullable disable

namespace WarehouseManagement.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? QuantityInStock { get; set; }
        public int? CategoryId { get; set; }
        public string CreatedBy { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
