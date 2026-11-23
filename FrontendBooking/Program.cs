using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FrontendBooking;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddRadzenComponents();

//TODO fix baseaddress
builder.Services.AddScoped(sp => new HttpClient
{
    //BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    BaseAddress = new Uri("http://localhost:5108/")
});


builder.Services.AddScoped<FrontendBooking.Services.SelectedRoomService>();

await builder.Build().RunAsync();