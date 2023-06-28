using MonkFocusApp.Commands;
using MonkFocusApp.Views;
using MonkFocusDataAccess;
using MonkFocusRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using MonkFocusModels;

namespace MonkFocusApp.ViewModels
{
    class RegisterViewModel : BaseViewModel
    {
        private string _username;
        private string _password;
        private string _name;
        private string _email;
        private string _city;
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
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email!= value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }
        public string City
        {
            get { return _city; }
            set
            {
                if (_city != value)
                {
                    _city = value;
                    OnPropertyChanged(nameof(City));
                }
            }
        }



        public ICommand LoginCommand { get; }

        public ICommand RegisterCommand { get; }

        private MonkFocusDbContext _context = new MonkFocusDbContext();
        private UserRepository _userRepository;
        public RegisterViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            RegisterCommand = new RelayCommand(Register);
            _userRepository = new UserRepository(_context);
        }

        private void Register()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(City))
            {
                MessageBox.Show("Please fill in all fields");
                return;
            }

            if (_userRepository.CheckifUserExists(Username))
            {
                MessageBox.Show("Username already exists");
                return;
            }

            if (Password.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long");
                return;
            }

            if (!IsValidEmail(Email))
            {
                MessageBox.Show("Invalid email format");
                return;
            }

            var user = new User()
            {
                Username = Username,
                Password = Password,
                Name = Name,
                Email = Email,
                City = City,
                JoinDate = DateTime.Now,
                Points = 0,
                WakeUpTime = new TimeOnly(8, 0, 0),
                BedTime = new TimeOnly(22, 0, 0),
                WorkTimeGoal = new TimeSpan(4, 0, 0)
            };

            _userRepository.AddUser(user);
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        ContentControl viewContainer = Application.Current.MainWindow.FindName("viewContainer") as ContentControl;
        private void Login()
        {
            LoginView registerViewModel = new LoginView();
            viewContainer.Content = registerViewModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
