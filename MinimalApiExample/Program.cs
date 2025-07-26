using MinimalApiExample;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/", () =>
{
    return "Hello World ";
});

app.MapPost("/", (Post post) =>
{
    return Results.Ok(post);

});
app.MapPut("/", (string name) =>
{
    return Results.Ok("Hello " + name);
});
app.MapDelete("/", () =>
{
    return Results.Ok("Call Delete Action Method");
});

app.Run();