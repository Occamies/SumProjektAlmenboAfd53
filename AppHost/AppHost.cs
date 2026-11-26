var builder = DistributedApplication.CreateBuilder(args);


// Opsætning af PostgresSQL database server i en docker container med persistent data.
var postgres = builder.AddPostgres("postgres")
    .WithDataVolume()
    .WithPgAdmin();

// Opretter en database på database serveren.
var db = postgres.AddDatabase("postgresDB");

// Kører migration workeren for at opdatere migrations.
var migrations = builder.AddProject<Projects.MigrationWorker>("migrations")
    .WithReference(db)
    .WaitFor(db);

// API med db reference. - Kører først når migrationworker er færdig.
var api = builder.AddProject<Projects.API>("api")
    .WithReference(db)
    .WithReference(migrations)
    .WaitForCompletion(migrations);

// Blazor frontend - Kører først når api'en kører.
builder.AddProject<Projects.FrontendBooking>("frontend")
    .WithReference(api)
    .WaitForStart(api);

builder.Build().Run();