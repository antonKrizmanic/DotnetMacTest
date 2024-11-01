using DontetMacTest.Shared.DTOs;

namespace DotnetMacTest.Shared;

public interface IMainApiService
{
    Task SeedAsync();
    Task<IEnumerable<PersonDto>> GetPeopleAsync();
    Task<ExportResultDto> ExportPeopleToExcelAsync();
    Task<ExportResultDto> ExportPeopleToPdfAsync();
    Task<ExportResultDto> ExportWordAsync();
}
