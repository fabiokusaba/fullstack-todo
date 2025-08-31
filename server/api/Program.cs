using api;
using api.Exceptions;
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

builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddCors();

var app = builder.Build();

app.UseExceptionHandler();

app.UseCors(config =>
{
    config.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin()
        .SetIsOriginAllowed(x => true);
});

app.MapControllers();

app.UseOpenApi();
app.UseSwaggerUi();

app.Run();
