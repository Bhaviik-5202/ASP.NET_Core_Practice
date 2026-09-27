namespace ProductManagementAPI.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string ProductCode { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public DateTime ManufactureDate { get; set; } = DateTime.Now;

        public DateTime ExpiryDate { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}
