using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProductApi.Context;
using ProductApi.Extensions;
using ProductApi.Loggers;
using ProductApi.Managers;
using ProductApi.PaginationHelper;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProductDbContext>(options =>
{
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    options.UseNpgsql("Server=product_db;Port=5432;Database=product_db;User Id=postgres; Password=postgres");
    //options.UseInMemoryDatabase("ProductDb");
});
builder.Services.AddScoped<IProductManager, ProductManager>();

var logger = ErrorLogger.WriteLogToFile(builder.Configuration, $"Loggers/{DateTime.Now:dd/MM/yyyy}.txt");
builder.Logging.AddSerilog(logger);
var app = builder.Build();

// Configure the HTTP request pipeline.


    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();


app.UseAuthorization();
app.MapControllers();

HttpContextHelper.Configure(app.Services.GetService<IHttpContextAccessor>());
app.MigrateProductDb();

app.Run();
