using Microsoft.EntityFrameworkCore;
using Models;
namespace DataHandler;

public class DatabaseContext : DbContext
{
	public DatabaseContext(DbContextOptions<DatabaseContext> options)
		: base(options)
	{
    }

	public DbSet<Employee> Employees { get; set; }
}
