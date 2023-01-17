using Hemiptera_API.Models;
using Hemiptera_API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(
    (sp, options) =>
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        // Services for AuditableEntitySaveChangesInterceptor
       // var dateTimeService = sp.GetService<IDateTimeService>()!;
       // var userIdentifierService = sp.GetService<IUserIdentifierService>()!;

       // options.AddInterceptors(new AuditableEntitySaveChangesInterceptor(dateTimeService, userIdentifierService));
        options.UseSqlServer(connectionString);
    });

builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Create}/{action=Create}/{id?}");
    
app.MapControllers();

app.Run();
