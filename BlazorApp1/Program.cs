using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorApp1.Services;
using BlazorApp1.Components;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register Typed Client (Make sure the URL matches your API's running port)
builder.Services.AddHttpClient<StudentsApiClient>(client => 
    client.BaseAddress = new Uri("https://localhost:7000")); // Update port!

// Register State Container
builder.Services.AddScoped<StateContainer>();

await builder.Build().RunAsync();