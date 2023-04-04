using System.Collections.Generic;
using System.Linq;

namespace WarehouseManagement.Models
{
    public class OrderModel
    {
        public List<OrderDetails> OrderDetails { get; set; }
        public int OrderId { get; set; }
        public string SelectedItem { get; set; }
        public int CustomerId { get; set; }
        public double totalMoney()
        {
            double totalMoney = 0;
            foreach (var item in OrderDetails)
            {
                totalMoney += item.total();
            }
            return totalMoney;
        }
    }
}
