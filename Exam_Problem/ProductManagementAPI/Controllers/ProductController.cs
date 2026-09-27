using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Data;
using ProductManagementAPI.Models;

namespace ProductManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();

            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            product.ProductId = 0;
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest("Product ID does not match.");
            }

            var existingProduct = await _context.Products.FindAsync(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.ProductName = product.ProductName;
            existingProduct.Price = product.Price;
            existingProduct.IsAvailable = product.IsAvailable;

            await _context.SaveChangesAsync();

            return Ok(existingProduct);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableProducts()
        {
            var products = await _context.Products
                .Where(p => p.IsAvailable)
                .ToListAsync();

            return Ok(products);
        }

        [HttpGet("price-range")]
        public async Task<IActionResult> GetProductsByPriceRange(
            [FromQuery] decimal min,
            [FromQuery] decimal max)
        {
            if (min > max)
            {
                return BadRequest("Minimum price cannot be greater than maximum price.");
            }

            var products = await _context.Products
                .Where(p => p.Price >= min && p.Price <= max)
                .ToListAsync();

            return Ok(products);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetProductSummary()
        {
            var totalProducts = await _context.Products.CountAsync();

            var availableProducts = await _context.Products
                .CountAsync(p => p.IsAvailable);

            var averagePrice = await _context.Products
                .Select(p => (decimal?)p.Price)
                .AverageAsync() ?? 0;

            return Ok(new
            {
                TotalProducts = totalProducts,
                AvailableProducts = availableProducts,
                AveragePrice = averagePrice
            });
        }
    }
}
