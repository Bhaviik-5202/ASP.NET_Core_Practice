namespace ProductManagementAPI.DTOs
{
    public class ProductDto
    {
        public class ProductCreateDto
        {
            public string ProductName { get; set; } = string.Empty;

            public string ProductCode { get; set; } = string.Empty;

            public string Category { get; set; } = string.Empty;

            public decimal Price { get; set; }

            public int StockQuantity { get; set; }

            public DateTime ManufactureDate { get; set; }

            public DateTime ExpiryDate { get; set; }

            public bool IsAvailable { get; set; }
        }

        public class ProductUpdateDto
        {
            public string ProductName { get; set; } = string.Empty;

            public string ProductCode { get; set; } = string.Empty;

            public string Category { get; set; } = string.Empty;

            public decimal Price { get; set; }

            public int StockQuantity { get; set; }

            public DateTime ManufactureDate { get; set; }

            public DateTime ExpiryDate { get; set; }

            public bool IsAvailable { get; set; }
        }

        public class ProductSummaryDto
        {
            public int TotalProducts { get; set; }
            public int AvailableProducts { get; set; }
            public decimal AveragePrice { get; set; }
        }
    }
}
