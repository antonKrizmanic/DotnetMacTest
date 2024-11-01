namespace DontetMacTest.Shared.DTOs;
public record PersonDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string OIB { get; set; }
    public IEnumerable<ContactDto> Contacts { get; set; }
}
