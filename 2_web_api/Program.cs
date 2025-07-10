var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/user", () => "Daniel Pardinho");

app.MapPost("/saveproduct", (Product product) =>
{
  return product.Code + " - " + product.Name;
});

app.Run();

public class Product
{
  public string Code { get; set; }
  public  string Name { get; set; }
}