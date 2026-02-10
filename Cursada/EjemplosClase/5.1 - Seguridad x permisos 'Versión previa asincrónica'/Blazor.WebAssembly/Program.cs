using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazor.WebAssembly;
using API.Clients;
using API.Auth.Blazor.WebAssembly;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configurar HttpClient para llamadas a la API
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7111/") });

// Configurar autenticación
builder.Services.AddSingleton<IAuthService, BlazorWasmAuthService>();

var app = builder.Build();

// Configurar AuthServiceProvider para ApiClients
var authService = app.Services.GetRequiredService<IAuthService>();
AuthServiceProvider.Register(authService);

await app.RunAsync();
