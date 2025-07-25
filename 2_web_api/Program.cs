using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/user", () => "Daniel Pardinho");

app.MapPost("/products", (Product product) =>
{
  ProductRepository.Add(product);

  return Results.Created($"/products/{product.Code}", product.Code);
});

app.MapGet("/products/{code}", ([FromRoute] string code) =>
{
  var product = ProductRepository.GetBy(code);

  if (product != null) return Results.Ok(product);

  return Results.NotFound();
});

app.MapPatch("/products", (Product product) =>
{
  var productSaved = ProductRepository.GetBy(product.Code);
  productSaved.Name = product.Name;

  return Results.Ok();

});

app.MapDelete("/products/{code}", ([FromRoute] string code) =>
{
  var product = ProductRepository.GetBy(code);
  ProductRepository.Remove(product);

  return Results.Ok();
});

app.MapGet("/configuration/database", (IConfiguration configuration) =>
{
  return Results.Ok($"{configuration["database:connection"]}/{configuration["database:port"]}");
});

app.Run();

public static class ProductRepository
{
  public static List<Product> Products { get; set; } = new();

  public static void Add(Product product)
  {

    Products.Add(product);
  }

  public static Product? GetBy(string code)
  {
    return Products.FirstOrDefault(p => p.Code == code);
  }

  public static void Remove(Product product)
  {
    Products.Remove(product);
  }
}

public class Product
{
  public string Code { get; set; }
  public string Name { get; set; }
}


public class ApplicationDbContext : DbContext
{
  public DbSet<Product> Products { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Products;User ID=sa;Password=@Sql2025;MultipleActiveResultSets=true;Encrypt=YES;TrustServerCertificate=True;");
}

/*

docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=@Sql2025" \
   -p 1433:1433 --name sqlserver --hostname sql1 \
   -d \
   mcr.microsoft.com/mssql/server:2025-latest

*/