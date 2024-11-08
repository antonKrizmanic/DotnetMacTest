using DotnetMacTest.Data;
using DotnetMacTest.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotnetMacTest.Services;

public class SeedService(ApplicationDbContext context) : ISeedService
{
    public async Task SeedPeople()
    {
        FakeData.InitPeople(1000);
        try
        {
            var fakePeople = FakeData.People;
            context.People.AddRange(fakePeople);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public async Task SeedContacts()
    {
        var peopleIds = await context.People.Select(x => x.Id).ToListAsync();
        FakeData.InitContacts(1000, peopleIds);

        try
        {
            var fakeContacts = FakeData.Contacts;
            context.Contacts.AddRange(fakeContacts);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}