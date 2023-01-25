using Hemiptera_API.Repositorys.Interfaces;
using Hemiptera_API.Services;
using Hemiptera_API.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Register Swagger with JWT Bearer authentication
builder.Services.RegisterSwaggerGen();

// Configure Database and JWT settings based on enviroment
if (builder.Environment.IsDevelopment())
{
    DbContextSettings.ConnectionString = builder.Configuration.GetConnectionString("DeveloperConnection")!;
    JwtSettings.Issuer = builder.Configuration["Jwt:DeveloperIssuer"]!;
    JwtSettings.Audience = builder.Configuration["Jwt:DeveloperAudience"]!;
    JwtSettings.IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:DeveloperKey"]!));
}
else
{
    DbContextSettings.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
    JwtSettings.Issuer = builder.Configuration["Jwt:Issuer"]!;
    JwtSettings.Audience = builder.Configuration["Jwt:Audience"]!;
    JwtSettings.IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!));
}

// Register different services to the application
builder.Services.RegisterAuthentication();
builder.Services.RegisterDbContext();
builder.Services.RegisterIdentity();
builder.Services.RegisterDependencyInjection();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Create}/{action=Create}/{id?}");

app.MapControllers();

app.Run();