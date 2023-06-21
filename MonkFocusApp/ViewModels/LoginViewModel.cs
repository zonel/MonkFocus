using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MonkFocusApp.Commands;
using MonkFocusDataAccess;
using MonkFocusRepositories;
using SQLitePCL;

namespace MonkFocusApp.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
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

        private void Login()
        {
            bool isAuthenticated = _userRepository.AuthenticateUser(Username, Password);

            if (isAuthenticated)
            {
                MessageBox.Show("Login successful!");
            }
            else
            {
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
