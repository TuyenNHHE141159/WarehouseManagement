using System;

namespace WebApplication1.Models
{
    public class OrderDetailDTO
    {
       public int OrderID { get; set; }
           public string                 OrderType { get; set; }
                 public DateTime           OrderDate { get; set; }
                    public string        OrderBy { get; set; }
                      public string      ProductName { get; set; }
                     public int       Quantiy { get; set; }
                          public double  Price { get; set; } 
    }
}
