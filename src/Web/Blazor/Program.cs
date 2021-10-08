using Commentor.GivEtPraj.Blazor;
using Commentor.GivEtPraj.Blazor.Services;
using Commentor.GivEtPraj.Blazor.Shared;
using Commentor.GivEtPraj.WebApi.Contracts.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Tewr.Blazor.FileReader;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:5001/v1/")
});
builder.Services.AddScoped<ICaseService, CaseService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddFileReaderService(o => o.UseWasmSharedBuffer = true);
builder.Services.AddScoped<ImageUpload>();
builder.Services.AddScoped<IGeoLocationService, GeoLocationService>();
builder.Services.AddValidatorsFromAssembly(typeof(CreateCaseRequest).Assembly);

await builder.Build().RunAsync();