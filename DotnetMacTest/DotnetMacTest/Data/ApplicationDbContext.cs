using Bogus;
using Microsoft.EntityFrameworkCore;

namespace DotnetMacTest.Data;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Person> People { get; set; }
    public DbSet<Contact> Contacts { get; set; }
}