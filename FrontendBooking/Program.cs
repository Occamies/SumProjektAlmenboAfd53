using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FrontendBooking;
using Radzen;

//Opretter blazor
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//Radzencomponents
builder.Services.AddRadzenComponents();

//Registrerer HttpClient med API'ets adresse.
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5108/")
});

//Registrerer SelectedRoomService, så den kan bruges i komponenterne.
builder.Services.AddScoped<FrontendBooking.Services.SelectedRoomService>();

//Starter applikationen.
await builder.Build().RunAsync();