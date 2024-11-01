using DontetMacTest.Shared.DTOs;
using DotnetMacTest.Data;
using DotnetMacTest.Services.Interfaces;
using DotnetMacTest.Shared;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace DotnetMacTest.ApiService;

public class MainApiService(
    ISeedService seedService, 
    IPeopleExcelExporter peopleExcelExporter,
    IPeoplePdfExporter peoplePdfExporter,
    IWordExporter wordExporter,
    ApplicationDbContext context) : IMainApiService
{
    public async Task SeedAsync()
    {
        await seedService.SeedPeople();
        await seedService.SeedContacts();
    }

    public async Task<IEnumerable<PersonDto>> GetPeopleAsync()
    {
        var people = await context.People.Include(p => p.Contacts).ToListAsync();

        return people.Adapt<IEnumerable<PersonDto>>();
    }
    
    public async Task<ExportResultDto> ExportPeopleToExcelAsync()
    {
        var people = await context.People.Include(p => p.Contacts).ToListAsync();
        var exportResult = peopleExcelExporter.ExportPeopleToExcelAsync(people);

        return exportResult;
    }

    public async Task<ExportResultDto> ExportPeopleToPdfAsync()
    {
        var people = await context.People.ToListAsync();

        return peoplePdfExporter.ExportPeopleToPdfAsync(people);
    }

    public async Task<ExportResultDto> ExportWordAsync()
    {
        var people = await context.People.Include(x => x.Contacts).ToListAsync();

        return wordExporter.ExportWord(people);
    }
}
