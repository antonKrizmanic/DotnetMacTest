using DontetMacTest.Shared.DTOs;
using DotnetMacTest.Data;

namespace DotnetMacTest.Services.Interfaces;

public interface IPeopleExcelExporter
{
    ExportResultDto ExportPeopleToExcelAsync(IEnumerable<Person> people);
}