using Microsoft.EntityFrameworkCore;
using system_zgloszen_ur.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<DataSeeder>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{

var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
 if (dbContext.Database.GetPendingMigrations().Any())
    {
        dbContext.Database.Migrate();
    }
   

var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    await seeder.SetUserAsync();
    await seeder.SetFailureAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();