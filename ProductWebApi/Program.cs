using Microsoft.EntityFrameworkCore;
using ProductWebApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ProductDbContext>(p =>
p.UseMySQL(builder.Configuration
.GetConnectionString("ConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
