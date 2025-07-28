using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 29))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Exemple d’API pour récupérer tous les produits
app.MapGet("/products", async (AppDbContext db) =>
{
    return await db.Products.ToListAsync();
})
.WithName("GetProducts");

// Exemple d’API pour récupérer un produit par id
app.MapGet("/products/{id}", async (int id, AppDbContext db) =>
{
    var product = await db.Products.FindAsync(id);
    return product is not null ? Results.Ok(product) : Results.NotFound();
})
.WithName("GetProductById");

// D’autres endpoints (POST, PUT, DELETE) peuvent être ajoutés ici
app.UseAuthorization();
app.MapControllers();
app.Run();