using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/user", () => "Daniel Pardinho");

app.MapPost("/saveproduct", (Product product) =>
{
  return product.Code + " - " + product.Name;
});

app.MapGet("/getproduct", ([FromQuery] string dateStart, [FromQuery] string dateEnd) =>
{
  return dateStart + " - " + dateEnd;
});

app.MapGet("/getproduct/{code}", ([FromRoute] string code) =>
{
  return code;
});

app.MapGet("/getproductbyheader", (HttpRequest request) =>
{
  return request.Headers["product-code"].ToString();
});

app.Run();

public static class ProductRepository
{
  public static List<Product> Products { get; set; }

  public static void Add(Product product)
  {
    if (Products == null)
      Products = new List<Product>();

    Products.Add(product);
  }

  public static Product GetBy(string code)
  {
    return Products.First(p => p.Code == code);
  }
}

public class Product
{
  public string Code { get; set; }
  public string Name { get; set; }
}