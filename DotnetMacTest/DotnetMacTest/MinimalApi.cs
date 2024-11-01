using DotnetMacTest.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DotnetMacTest;

public static class MinimalApi
{
    public static WebApplication UseMinimalApi(this WebApplication app)
    {
        app.MapGet("/seed", async ([FromServices] IMainApiService service) => await service.SeedAsync());
        app.MapGet("/people/get", async ([FromServices] IMainApiService service) => await service.GetPeopleAsync());
        app.MapGet("/people/export", async ([FromServices] IMainApiService service) => await service.ExportPeopleToExcelAsync());
        app.MapGet("/people/exportPdf", async ([FromServices] IMainApiService service) => await service.ExportPeopleToPdfAsync());
        app.MapGet("/people/exportWord", async ([FromServices] IMainApiService service) => await service.ExportWordAsync());
        
        return app;
    }
}
