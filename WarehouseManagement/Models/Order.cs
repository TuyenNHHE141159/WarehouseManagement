﻿using System;
using System.Collections.Generic;

#nullable disable

namespace WarehouseManagement.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public string OrderType { get; set; }
        public DateTime? OrderDate { get; set; }
        public string CreatedBy { get; set; }
        public string Note { get; set; }
        public int? Status { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
