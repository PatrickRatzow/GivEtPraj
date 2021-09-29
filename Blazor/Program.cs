using System;
using System.Net.Http;
using Blazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IGeoLocationService, GeoLocationService>();
builder.Services.AddScoped<ICameraService, CameraService>();
builder.Services.AddScoped<IServiceWorkerService, ServiceWorkerService>();

await builder.Build().RunAsync();
