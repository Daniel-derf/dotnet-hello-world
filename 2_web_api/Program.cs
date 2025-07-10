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

app.Run();

public class Product
{
  public string Code { get; set; }
  public  string Name { get; set; }
}