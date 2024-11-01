using DontetMacTest.Shared.DTOs;
using DotnetMacTest.Data;

namespace DotnetMacTest.Services.Interfaces;

public interface IWordExporter
{
    ExportResultDto ExportWord(IEnumerable<Person> people);
}