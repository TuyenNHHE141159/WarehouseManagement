namespace WebApplication1.Models
{
    public class OrderDetails
    {
        public OrderDetails() { }   
        public int productId { get; set; }
        public string productName { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }

        public double total()
        {
            return price*quantity;
        }
    }
}
