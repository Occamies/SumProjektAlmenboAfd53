var builder = DistributedApplication.CreateBuilder(args);


// Opsætning af PostgresSQL database server i en docker container.
var postgres = builder.AddPostgres("postgres")
    .WithDataVolume()
    .WithPgAdmin();

// Database
var db = postgres.AddDatabase("postgresDB");

var migrations = builder.AddProject<Projects.MigrationWorker>("migrations")
    .WithReference(db)
    .WaitFor(db);

// API med db reference.
var api = builder.AddProject<Projects.API>("api")
    .WithReference(db)
    .WithReference(migrations)
    .WaitForCompletion(migrations);

// Blazor frontend
builder.AddProject<Projects.BlazorFrontend>("frontend")
    .WithReference(api);

builder.Build().Run();