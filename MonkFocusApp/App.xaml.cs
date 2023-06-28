using System;
using System.Windows;
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
        var serviceProvider = ConfigureServices();
        var dbContext = new MonkFocusDbContextFactory().CreateDbContext();

        #region Seeding

        var DatabaseSeeder = new DatabaseSeeder(dbContext);
        DatabaseSeeder.SeedData();

        #endregion
    }

    private IServiceProvider ConfigureServices()
    {
        //Set up your dependency injection container and register services
        var services = new ServiceCollection();
        services.AddDbContext<MonkFocusDbContext>();

        return services.BuildServiceProvider();
    }
}