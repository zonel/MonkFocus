using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MonkFocusDataAccess;

/// <summary>
///     This class is used to create a database context for the database.
/// </summary>
public class MonkFocusDbContextFactory : IDesignTimeDbContextFactory<MonkFocusDbContext>
{
    public MonkFocusDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MonkFocusDbContext>();
        optionsBuilder.UseSqlite("DataSource=");

        return new MonkFocusDbContext(optionsBuilder.Options);
    }

    public MonkFocusDbContext CreateDbContext()
    {
        return new MonkFocusDbContext();
    }
}