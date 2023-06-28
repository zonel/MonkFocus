using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MonkFocusApp.ViewModels;
using MonkFocusApp.Views;
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

            #region Seeding
            var DatabaseSeeder = new DatabaseSeeder(dbContext);
            DatabaseSeeder.SeedData();
            #endregion

            // Create the main window
            //MainWindow mainWindow = new MainWindow();

            //// Create the login view
            //LoginView loginView = new LoginView();

            //// Set the login view as the content of the main window
            //mainWindow.Content = loginView;

            //// Create the navigation service and pass the current window to it
            //NavigationService navigationService = new NavigationService(MainWindow);
            //LoginViewModel loginViewModel = new LoginViewModel(navigationService);
            //loginView.DataContext = loginViewModel;

            //mainWindow.Show();
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
