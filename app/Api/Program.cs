using Api.Middlewares;
using Application.Configurations;
using Application.Configurations.Filters;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration
    .AddJsonFile("appsettings.json",
        optional: true,
        reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json",
        optional: true,
        reloadOnChange: true);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddRepositories();
builder.Services.AddDomainServices();
builder.Services.AddMapperProfiles();
builder.Services.AddNotificationsServices();
builder.Services.RegisterRequestHandler();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services
    .AddControllers(opt =>
    {
        opt.Filters.Add<NotificationFilter>();
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true; // Opcional
    });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Automatic Database Creation and Migration
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "v1");
});
//}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();