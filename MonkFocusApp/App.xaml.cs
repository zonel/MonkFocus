using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MonkFocusDataAccess;

namespace MonkFocusApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var serviceProvider = ConfigureServices();

            var dbContext = new MonkFocusDbContextFactory().CreateDbContext();
            var DatabaseSeeder = new DatabaseSeeder(dbContext);
            DatabaseSeeder.SeedData();

        }

        private IServiceProvider ConfigureServices()
        {
            //Set up your dependency injection container and register services
           var services = new ServiceCollection();
           services.AddDbContext<MonkFocusDbContext>();

            // Register the MonkFocusDbContext and other services
            //services.AddDbContext<MonkFocusDbContext>(options =>
            //    options.UseSqlite("DataSource=Monkfocus.db"));

            //services.AddTransient<DatabaseSeeder>();

            // Add other services and repositories

            // Build the service provider
            return services.BuildServiceProvider();
        }
    }
}
