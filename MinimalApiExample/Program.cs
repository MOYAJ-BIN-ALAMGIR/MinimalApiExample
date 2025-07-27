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

app.MapPost("/posts", (Post post, ApplicationDbContext db) =>
{
    db.Posts.Add(post);
    bool issaved = db.SaveChanges() > 0;
    if (issaved)
    {
        return Results.Ok("Post has been made.");
    }
    return Results.BadRequest("post saved faild.");
});

app.MapPut("/posts", (int id, Post post, ApplicationDbContext db) =>
    {
        var data = db.Posts.FirstOrDefault(c => c.Id == id);
        if(data == null)
        {
            return Results.NotFound();
        }
        if(data.Id != post.Id)
        {
            return Results.BadRequest("Id not valid");
        }
        data.Title = post.Title;
        data.Description = post.Description;
        bool isupdated = db.SaveChanges() > 0;
        if(isupdated)
        {
            return Results.Ok("Posts has been modified.");
        }
        return Results.BadRequest("Post modified faild");
            
    });
app.MapDelete("/posts", (int id, ApplicationDbContext db) =>
{
    var post = db.Posts.FirstOrDefault(c => c.Id == id);
    if (post == null)
    {
        return Results.NotFound();
    }
    db.Posts.Remove(post);
    if (db.SaveChanges() > 0)
    {
        return Results.Ok("Post has been deleted.");
    }
    return Results.BadRequest("Post delet failed.");
});

app.Run();