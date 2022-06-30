using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Nubex_Client;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjU5NTM5QDMyMzAyZTMxMmUzMGpyd1p6cE9sN3JmNU1mamt4d2NDd2c3Z1hGNTA4enNnNWhtbWpWZm53dm89");
await builder.Build().RunAsync();
