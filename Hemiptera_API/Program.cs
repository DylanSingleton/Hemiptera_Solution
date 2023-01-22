using Hemiptera_API.Models;
using Hemiptera_API.Services;
using Hemiptera_API.Services.Interfaces;
using Hemiptera_API.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Hemiptera",
        Description = "Hemiptera WebAPI"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new string[]{}
        }
    }); 
});

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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = JwtSettings.ValidateIssuer,
            ValidateAudience = JwtSettings.ValidateAudience,
            ValidateLifetime = JwtSettings.ValidateLifetime,
            ValidateIssuerSigningKey = JwtSettings.ValidateIssuerSigningKey,
            ValidIssuer = JwtSettings.Issuer,
            ValidAudience = JwtSettings.Audience,
            IssuerSigningKey = JwtSettings.IssuerSigningKey
        };
    });

builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddTransient(typeof(IGenericService<>), typeof(GenericService<>));
builder.Services.AddTransient<IProjectService, ProjectService>();
builder.Services.AddTransient<IUnitOfWorkService, UnitOfWorkService>();

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