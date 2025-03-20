using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Persistence
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = Guid.NewGuid(), Name = "Smartphone", Description = "Latest 5G smartphone", Price = 699, StockQuantity = 50, ImageUrl = "smartphone.jpg", Category = "Electronics" },
                new Product { Id = Guid.NewGuid(), Name = "Laptop", Description = "Powerful laptop for work", Price = 999, StockQuantity = 30, ImageUrl = "laptop.jpg", Category = "Electronics" }
            );
        }
    }
}
