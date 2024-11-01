using DontetMacTest.Shared.DTOs;
using DotnetMacTest.Shared;
using System.Net.Http.Json;

namespace DotnetMacTest.Client.HttpService;

public class MainHttpService(IHttpClientFactory httpClientFactory) : IMainApiService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("BlazorServerHttpClient");

    public async Task SeedAsync()
    {
        await _httpClient.GetAsync("/seed");
    }

    public async Task<IEnumerable<PersonDto>> GetPeopleAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<PersonDto>>("/people/get");
    }
    
    public async Task<ExportResultDto> ExportPeopleToExcelAsync()
    {
        return await _httpClient.GetFromJsonAsync<ExportResultDto>("/people/export");
    }

    public async Task<ExportResultDto> ExportPeopleToPdfAsync()
    {
        return await _httpClient.GetFromJsonAsync<ExportResultDto>("/people/exportPdf");
    }

    public async Task<ExportResultDto> ExportWordAsync()
    {
        return await _httpClient.GetFromJsonAsync<ExportResultDto>("/people/exportWord");
    }
}
