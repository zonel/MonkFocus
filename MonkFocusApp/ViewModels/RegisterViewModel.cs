using System;
using System.ComponentModel;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MonkFocusApp.Commands;
using MonkFocusApp.Views;
using MonkFocusDataAccess;
using MonkFocusModels;
using MonkFocusRepositories;
using BC = BCrypt.Net.BCrypt;


namespace MonkFocusApp.ViewModels;

/// <summary>
///     This class is the view model for the register view.
/// </summary>
internal class RegisterViewModel : BaseViewModel
{
    private string _city;

    private readonly MonkFocusDbContext _context = new();
    private string _email;
    private string _name;
    private string _password;
    private string _username;
    private readonly UserRepository _userRepository;

    private readonly ContentControl viewContainer =
        Application.Current.MainWindow.FindName("viewContainer") as ContentControl;

    public RegisterViewModel()
    {
        LoginCommand = new RelayCommand(NavigateToLoginView);
        RegisterCommand = new RelayCommand(Register);
        _userRepository = new UserRepository(_context);
    }

    public string Username
    {
        get => _username;
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
        get => _password;
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
        get => _name;
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
        get => _email;
        set
        {
            if (_email != value)
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
    }

    public string City
    {
        get => _city;
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

    /// <summary>
    ///     This method registers a new user.
    /// </summary>
    private void Register()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password) ||
            string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(City))
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

        
        var user = new User
        {
            Username = Username,
            Password = BC.HashPassword(Password),
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
        MessageBox.Show("Registration successful");
        NavigateToLoginView();
    }

    /// <summary>
    ///     This method checks if the email is in a valid format.
    /// </summary>
    /// <param name="email">user's email.</param>
    /// <returns>True if parametr is in valid format.</returns>
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

    /// <summary>
    /// This method navigates to the login view.
    /// </summary>
    private void NavigateToLoginView()
    {
        var registerViewModel = new LoginView();
        viewContainer.Content = registerViewModel;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}