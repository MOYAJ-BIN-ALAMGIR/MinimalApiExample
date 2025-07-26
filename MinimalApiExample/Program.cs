using Microsoft.EntityFrameworkCore;
using MinimalApiExample;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseInMemoryDatabase("TestDB");
});

var app = builder.Build();

app.MapGet("/", () =>
{
    return "Hello World ";
});

app.MapPost("/api/post", (Post post) =>
{
    return Results.Ok(post);

});
app.MapPut("/api/put", (string name) =>
{
    return Results.Ok("Hello " + name);
});
app.MapDelete("/", () =>
{
    return Results.Ok("Call Delete Action Method");
});

app.MapGet("/posts", (ApplicationDbContext db) =>
    {
        var posts = db.Posts.ToList();
        return Results.Ok(posts);
});

app.Run();