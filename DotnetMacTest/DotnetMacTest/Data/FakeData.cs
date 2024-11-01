using Bogus;

namespace DotnetMacTest.Data;

public class FakeData
{
    public static List<Person> People = new List<Person>();
    public static List<Contact> Contacts = new List<Contact>();

    public static void InitPeople(int count)
    {
        var peopleFaker = new Faker<Person>()
            .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName())
            .RuleFor(u => u.LastName, (f, u) => f.Name.LastName())
            .RuleFor(u => u.OIB, (f, u) => f.Random.Number(10000000, 999999999).ToString());

        var people = peopleFaker.Generate(count);

        FakeData.People.AddRange(people);
    }

    public static void InitContacts(int count, List<int> peopleIds)
    {
        var contactFaker = new Faker<Contact>()
            .RuleFor(p => p.Address, f => f.Address.StreetAddress())
            .RuleFor(p => p.Email, f => f.Internet.Email())
            .RuleFor(p => p.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(p => p.MobilePhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(p => p.PersonId, f => f.PickRandom(peopleIds));

        var contacts = contactFaker.Generate(count);
        
        FakeData.Contacts.AddRange(contacts);
    }
}

