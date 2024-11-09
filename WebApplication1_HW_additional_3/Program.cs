using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

var app = WebApplication.CreateBuilder(args).Build();

var customers = new List<Customer>
{
    new Customer { Id = 1, Name = "Bob1" },
    new Customer { Id = 2, Name = "Bob2" },
    new Customer { Id = 3, Name = "Bob3" }
};

app.Run(async context =>
{
    if (context.Request.Path == "/api/customers" && context.Request.Method == "GET")
    {
        var json = System.Text.Json.JsonSerializer.Serialize(customers);
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(json);
    }
});

app.Run();

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
}