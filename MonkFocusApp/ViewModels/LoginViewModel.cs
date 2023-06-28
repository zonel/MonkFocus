using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MonkFocusApp.Commands;
using MonkFocusApp.Views;
using MonkFocusDataAccess;
using MonkFocusRepositories;

namespace MonkFocusApp.ViewModels; 

/// <summary>
/// This class is the view model for the login view.
/// </summary>
internal class LoginViewModel : BaseViewModel
{
    private readonly MonkFocusDbContext _context = new();
    private string _password;
    private string _username;
    private readonly UserRepository _userRepository;

    private readonly ContentControl viewContainer =
        Application.Current.MainWindow.FindName("viewContainer") as ContentControl;

    public LoginViewModel()
    {
        LoginCommand = new RelayCommand(Login);
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

    public ICommand LoginCommand { get; }

    public ICommand RegisterCommand { get; }

    private void Register()
    {
        var registerViewModel = new RegisterView();
        viewContainer.Content = registerViewModel;
    }

    private void Login()
    {
        var isAuthenticated = _userRepository.AuthenticateUser(Username, Password);

        if (isAuthenticated)
        {
            var UserId = _userRepository.GetUserByUsername(Username).UserId;
            var dashboardViewModel = new DashboardView(UserId, _context);
            viewContainer.Content = dashboardViewModel;
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