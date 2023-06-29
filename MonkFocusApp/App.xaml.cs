using System;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MonkFocusDataAccess;

namespace MonkFocusApp;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        using (var dbContext = new MonkFocusDbContext())
        {
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                dbContext.Database.Migrate();
            }

            var DatabaseSeeder = new DatabaseSeeder(dbContext);
            DatabaseSeeder.SeedData();
        }
    }

    private IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddDbContext<MonkFocusDbContext>();

        return services.BuildServiceProvider();
    }
}