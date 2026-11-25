using API.Contexts;
using Microsoft.EntityFrameworkCore;

// Opretter en "builder", der samler alle indstillinger og services til API'et
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Sørger for, at API'et finder og bruger controllerne
builder.Services.AddControllers();
// Genererer en OpenAPI-beskrivelse af jeres endpoints (en maskinlæsbar oversigt over API'et)
builder.Services.AddOpenApi();

// Registrerer BookingContext (databasen) og forbinder til PostgreSQL.
// "postgresDB" er navnet på databasen i AppHost, og Aspire leverer connection string'en herfra.
// Navnet skal være det samme som i AppHost.
builder.AddNpgsqlDbContext<BookingContext>(connectionName: "postgresDB");

// CORS: Browseren blokerer som standard kald fra én adresse (frontend) til en anden (API).
// Her laves en regel, som tillader frontend'en at kalde API'et.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5015") // kun kald fra denne adresse er tilladt
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Bygger selve applikationen ud fra alt det ovenstående.
// Efter denne linje kan man ikke længere tilføje services.
var app = builder.Build();

// Aktiverer CORS-reglen "AllowFrontend" fra før
app.UseCors("AllowFrontend");

// Udstiller OpenAPI-beskrivelsen, men kun under udvikling (ikke i en færdig version)
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Sender automatisk http-kald videre til https
app.UseHttpsRedirection();

// Tjekker rettigheder på endpoints
app.UseAuthorization();

// Forbinder controllernes routes (fx api/reservation) med API'et
app.MapControllers();

// Starter serveren og lytter efter requests
app.Run();