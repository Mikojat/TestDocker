using CustomerWebApi.Data;
using JwtAuthenticationManager;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

/*var host = Environment.GetEnvironmentVariable("DB_HOST");
var name = Environment.GetEnvironmentVariable("DB_NAME");
var password = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");

var connection = $"Data Source={host};Initial Catalog={name};User ID=sa;Password={password}";
*/
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCustomJwtAuthentication();

builder.Services.AddDbContext<CustomerDbContext>(options =>
    options.UseSqlServer(builder.Configuration
    .GetConnectionString("ConnectionString")));


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
