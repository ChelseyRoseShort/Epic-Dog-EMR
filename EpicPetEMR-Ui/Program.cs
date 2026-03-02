using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using EpicPetEMR_Ui;
using EpicPetEMR_Ui.Services;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://petchanceemr.chelseyshort.com/") });
builder.Services.AddScoped<PetApi>();
builder.Services.AddScoped<EpicPetEMR_Ui.Services.TokenStore>();
builder.Services.AddScoped<EpicPetEMR_Ui.Services.AuthApi>();

builder.Services.AddScoped<MedicationScheduleService>();


await builder.Build().RunAsync();
