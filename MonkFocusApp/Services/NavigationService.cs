using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MonkFocusApp.Services.Interfaces;
using MonkFocusApp.Views;

namespace MonkFocusApp.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Window _currentWindow;

        public NavigationService(Window currentWindow)
        {
            _currentWindow = currentWindow;
        }
        public void NavigateToMainScreen()
        {
            Dashboard Dashboard = new Dashboard();
            Dashboard.DataContext = new ViewModels.DashboardViewModel();
            _currentWindow.Content = Dashboard;
        }
    }
}
