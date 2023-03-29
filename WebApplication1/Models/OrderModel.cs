using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class OrderModel
    {
        public List<OrderDetails> OrderDetails { get; set; }
        public string SelectedItem { get; set; }
    }
}
