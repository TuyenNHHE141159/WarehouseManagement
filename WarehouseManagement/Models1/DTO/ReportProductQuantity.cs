namespace WarehouseManagement.Models.DTO
{
    public class ReportProductQuantity
    {
        public int Year { get; set; }   
        public int Month { get; set; }
        public string ProductName { get; set; }
        public int TotalQuantity { get; set; }
    }
}
