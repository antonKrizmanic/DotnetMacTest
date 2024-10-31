﻿using DotnetMacTest.Client.HttpService;
using DotnetMacTest.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace DotnetMacTest.Client;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomHttpClientFactory(this IServiceCollection services, WebAssemblyHostBuilder builder)
    {
        services.AddHttpClient("BlazorServerHttpClient",
                client => client.BaseAddress = new Uri("https://localhost:7053/"));
        return services;
    }

    public static IServiceCollection AddHttpServices(this IServiceCollection services)
    {
        services.AddScoped<IMainApiService, MainHttpService>();
        return services;
    }
}