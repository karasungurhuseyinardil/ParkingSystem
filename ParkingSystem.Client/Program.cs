using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ParkingSystem.Client;
using ParkingSystem.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Assuming API runs on localhost:5000 (http) or 5001 (https). 
// I'll set it to the base address of the host if hosted, but here they are separate.
// I'll hardcode 5000 for now, but in a real scenario, I'd use config.
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5018") }); 
builder.Services.AddScoped<ParkingApiClient>();
builder.Services.AddScoped<DragState>();

await builder.Build().RunAsync();
