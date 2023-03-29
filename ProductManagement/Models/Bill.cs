using System;
using System.Collections.Generic;

#nullable disable

namespace ProductManagement.Models
{
    public partial class Bill
    {
        public Bill()
        {
            BillDetails = new HashSet<BillDetail>();
        }

        public string BillId { get; set; }
        public string BillType { get; set; }
        public string CustomerId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? PaidDate { get; set; }
        public bool? PaidStatus { get; set; }

        public virtual BillType BillTypeNavigation { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<BillDetail> BillDetails { get; set; }
    }
}
