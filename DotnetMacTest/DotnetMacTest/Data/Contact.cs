namespace DotnetMacTest.Data;

public class Contact
{
    public int Id { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string MobilePhoneNumber { get; set; }
    public int PersonId { get; set; }
    public Person Person { get; set; }
}
