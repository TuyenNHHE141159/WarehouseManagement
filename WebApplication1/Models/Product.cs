using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
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

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
