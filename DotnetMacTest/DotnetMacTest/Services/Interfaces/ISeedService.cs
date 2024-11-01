namespace DotnetMacTest.Services.Interfaces;

public interface ISeedService
{
    Task SeedPeople();
    Task SeedContacts();
}