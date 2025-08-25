using api;
using efscaffold.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var appOptions = builder.Services.AddAppOptions(builder.Configuration);

builder.Services.AddDbContext<MyDbContext>(conf =>
{
    conf.UseNpgsql(appOptions.DbConnectionString);
});

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(config =>
{
    config.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin()
        .SetIsOriginAllowed(x => true);
});

app.MapGet("/", ([FromServices] MyDbContext dbContext) =>
{
    var myTodo = new Todo()
    {
        Title = "test",
        Description = "test",
        Id = Guid.NewGuid().ToString(),
        Isdone = false,
        Priority = 5
    };
    dbContext.Todos.Add(myTodo);
    dbContext.SaveChanges();
    var objects = dbContext.Todos.ToList();
    return objects;
});

app.Run();
