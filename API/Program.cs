using API;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// builder.Services.AddDbContext<BookingContext>(options =>
//     options.UseNpgsql(                                                      // Fortæller framework at vi vil bruge PostgreSQL
//         builder.Configuration.GetConnectionString("database")  // Finder connectio til appsetting.json
//     )
// );

builder.AddNpgsqlDbContext<BookingContext>(connectionName: "database");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();