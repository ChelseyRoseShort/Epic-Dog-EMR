using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using EpicPetEMR_Ui;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using EpicPetEMR_Ui.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register the auth handler
builder.Services.AddTransient<AuthHeaderHandler>();

// Authenticated HttpClient — all API services use this
builder.Services.AddHttpClient("AuthenticatedClient", client =>
    client.BaseAddress = new Uri("http://localhost:5001/"))
    .AddHttpMessageHandler<AuthHeaderHandler>();

// API services
builder.Services.AddScoped<PetApi>(sp =>
    new PetApi(sp.GetRequiredService<IHttpClientFactory>().CreateClient("AuthenticatedClient")));
builder.Services.AddScoped<FamilyApi>(sp =>
    new FamilyApi(sp.GetRequiredService<IHttpClientFactory>().CreateClient("AuthenticatedClient")));
builder.Services.AddScoped<AuthApi>(sp =>
    new AuthApi(sp.GetRequiredService<IHttpClientFactory>().CreateClient("AuthenticatedClient")));

// Other services
builder.Services.AddScoped<MedicationScheduleService>();
builder.Services.AddScoped<TokenStore>();

await builder.Build().RunAsync();