using DotnetMacTest.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DotnetMacTest;

public static class MinimalApi
{
    public static WebApplication UseMinimalApi(this WebApplication app)
    {
        app.MapGet("/get", async ([FromServices] IMainApiService service) => await service.GetAsync());

        return app;
    }
}
