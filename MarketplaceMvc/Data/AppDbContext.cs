
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext {
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}

public class Product
{
}


public class User
{
}