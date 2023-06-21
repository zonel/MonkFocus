using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MonkFocusApp.Commands;
using MonkFocusApp.Services;
using MonkFocusApp.Services.Interfaces;
using MonkFocusApp.Views;
using MonkFocusDataAccess;
using MonkFocusRepositories;
using SQLitePCL;

namespace MonkFocusApp.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        private readonly NavigationService _navigationService;

        public LoginViewModel(NavigationService navigationService)
        {
            LoginCommand = new RelayCommand(Login);
            _userRepository = new UserRepository(_context);
            _navigationService = navigationService;
        }

        private string _username;
        private string _password;

        public string Username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public ICommand LoginCommand { get; }

        private MonkFocusDbContext _context = new MonkFocusDbContext();
        private UserRepository _userRepository;
        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            _userRepository = new UserRepository(_context);
        }

        ContentControl viewContainer = Application.Current.MainWindow.FindName("viewContainer") as ContentControl;
        private void Login()
        {
            bool isAuthenticated = _userRepository.AuthenticateUser(Username, Password);

            if (isAuthenticated)
            {
                DashboardView dashboardViewModel = new DashboardView();
                viewContainer.Content = dashboardViewModel;
            }
            else
            {
                Password = "";
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
