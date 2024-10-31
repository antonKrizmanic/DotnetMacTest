using DotnetMacTest.Shared;

namespace DotnetMacTest.Client.HttpService;

public class MainHttpService : IMainApiService
{
    private readonly HttpClient _httpClient;

    public MainHttpService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("BlazorServerHttpClient");
    }

    public async Task<string> GetAsync()
    {
        return await _httpClient.GetStringAsync("/get");
    }
}
