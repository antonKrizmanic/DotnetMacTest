using DontetMacTest.Shared.DTOs;
using DotnetMacTest.Data;

namespace DotnetMacTest.Services.Interfaces;

public interface IPeoplePdfExporter
{
    ExportResultDto ExportPeopleToPdfAsync(IEnumerable<Person> people);
}