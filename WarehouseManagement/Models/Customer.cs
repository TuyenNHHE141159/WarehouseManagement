using System;
using System.Collections.Generic;

#nullable disable

namespace WarehouseManagement.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public int? AccountId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public virtual Account Account { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
