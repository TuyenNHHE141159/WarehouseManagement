using System;
using System.Collections.Generic;

#nullable disable

namespace WarehouseManagement.Models
{
    public partial class Bill
    {
        public Bill()
        {
            BillLines = new HashSet<BillLine>();
        }

        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string Paymentmethod { get; set; }
        public int CustomerId { get; set; }
        public int? Status { get; set; }
        public double? PaidMoney { get; set; }
        public DateTime? PaidDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<BillLine> BillLines { get; set; }
    }
}
