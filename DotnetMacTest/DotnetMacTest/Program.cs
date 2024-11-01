using DontetMacTest.Shared.FrontedService;
using DotnetMacTest;
using DotnetMacTest.ApiService;
using DotnetMacTest.Components;
using DotnetMacTest.Data;
using DotnetMacTest.Services;
using DotnetMacTest.Services.Interfaces;
using DotnetMacTest.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddFluentUIComponents();

// Custom services
builder.Services.AddScoped<IMainApiService, MainApiService>();
builder.Services.AddScoped<ISeedService, SeedService>();
builder.Services.AddScoped<IPeopleExcelExporter, PeopleExcelExporter>();
builder.Services.AddScoped<IPeoplePdfExporter, PeoplePdfExporter>();
builder.Services.AddScoped<IWordExporter, WordExporter>();
builder.Services.AddScoped<IJsService, JsService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseMinimalApi();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(DotnetMacTest.Client._Imports).Assembly);

app.Run();
