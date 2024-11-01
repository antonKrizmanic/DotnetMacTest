using DotnetMacTest.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddFluentUIComponents()
    .AddCustomHttpClientFactory(builder)
    .AddHttpServices()
    .AddFrontendServices();

await builder.Build().RunAsync();
