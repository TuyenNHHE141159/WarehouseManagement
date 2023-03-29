using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }
        public string OrderType { get; set; }
        public DateTime? OrderDate { get; set; }
        public string CreatedBy { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
