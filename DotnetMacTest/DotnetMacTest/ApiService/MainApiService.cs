using DotnetMacTest.Shared;

namespace DotnetMacTest.ApiService;

public class MainApiService : IMainApiService
{
    public async Task<string> GetAsync()
    {
        return "Hello world";
    }
}
