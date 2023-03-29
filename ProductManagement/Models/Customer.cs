using System;
using System.Collections.Generic;

#nullable disable

namespace ProductManagement.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Bills = new HashSet<Bill>();
        }

        public string CustomerId { get; set; }
        public string CustomerName { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}
