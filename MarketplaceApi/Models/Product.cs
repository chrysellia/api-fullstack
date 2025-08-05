using Microsoft.EntityFrameworkCore;

namespace MarketplaceApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Initialiser à une chaîne vide
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty; // Initialiser à une chaîne vide
        public int CategoryId { get; set; }
    }

    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
