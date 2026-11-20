using MigrationWorker;
using API.Contexts;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.AddNpgsqlDbContext<BookingContext>(connectionName: "postgresDB");

var host = builder.Build();
host.Run();