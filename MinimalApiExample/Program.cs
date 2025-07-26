var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/", () =>
{
    return "Hello World ";
});

app.MapPost("/", () =>
{
    return Results.Ok("Call post action method");

});
app.MapPut("/", () =>
{
    return Results.Ok("Call put Action Method");
});
app.MapDelete("/", () =>
{
    return Results.Ok("Call Delete Action Method");
});

app.Run();