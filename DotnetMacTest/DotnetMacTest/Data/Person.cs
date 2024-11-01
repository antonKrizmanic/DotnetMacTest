namespace DotnetMacTest.Data;

public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string OIB { get; set; }
    public virtual IEnumerable<Contact> Contacts { get; set; }
}
