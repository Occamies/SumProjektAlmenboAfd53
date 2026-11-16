var builder = DistributedApplication.CreateBuilder(args);


// Opsætning af PostgresSQL database i en docker container.
var postgres = builder.AddPostgres("postgres")
    .WithDataVolume()
    .WithPgAdmin();

// Database
var db = postgres.AddDatabase("database");

// API med db reference.
var api = builder.AddProject<Projects.API>("api")
    .WithReference(db);

// Blazor frontend
builder.AddProject<Projects.BlazorFrontend>("frontend")
    .WithReference(api);

builder.Build().Run();