using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Models;

namespace ProductManagementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(product => product.ProductId)
                    .ValueGeneratedOnAdd();

                entity.Property(product => product.Price)
                    .HasPrecision(18, 2);
            });
        }
    }
}
