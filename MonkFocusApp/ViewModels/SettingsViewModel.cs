using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using MonkFocusApp.Commands;
using MonkFocusDataAccess;
using MonkFocusRepositories;

namespace MonkFocusApp.ViewModels;

/// <summary>
///     This is view model for the settings page.
/// </summary>
internal class SettingsViewModel : BaseViewModel
{
    private readonly int _userId;

    private readonly UserRepository _userRepository;
    private string _bedtime;
    private string _wakeuptime;
    private string _worktimegoal;


    public SettingsViewModel(int userId)
    {
        _userId = userId;
        _userRepository = new UserRepository(new MonkFocusDbContext());

        WakeUpTimeSaveCommand = new RelayCommand(SaveWakeUpTime);
        BedTimeSaveCommand = new RelayCommand(BedTimeSave);
        WorkTimeGoalSaveCommand = new RelayCommand(WorkTimeSave);
    }

    public ICommand WakeUpTimeSaveCommand { get; }
    public ICommand BedTimeSaveCommand { get; }
    public ICommand WorkTimeGoalSaveCommand { get; }

    public string WakeUpTime
    {
        get => _wakeuptime;
        set
        {
            if (_wakeuptime != value)
            {
                _wakeuptime = value;
                OnPropertyChanged();
            }
        }
    }

    public string BedTime
    {
        get => _bedtime;
        set
        {
            if (_bedtime != value)
            {
                _bedtime = value;
                OnPropertyChanged();
            }
        }
    }

    public string WorkTimeGoal
    {
        get => _worktimegoal;
        set
        {
            if (_worktimegoal != value)
            {
                _worktimegoal = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    ///     This method saves the work time goal to the database.
    /// </summary>
    private void WorkTimeSave()
    {
        TimeSpan timeOnly;
        var pattern = @"^(0?[0-9]|1[0-9]|2[0-3]):([0-5][0-9])$";
        var match = Regex.Match(WorkTimeGoal, pattern);

        if (match.Success)
        {
            var hours = int.Parse(match.Groups[1].Value);
            var minutes = int.Parse(match.Groups[2].Value);

            var timeSpan = new TimeSpan(hours, minutes, 0);

            _userRepository.UpdateUsersWorkTimeGoal(_userId, timeSpan);
            MessageBox.Show("Work Time Goal saved!");
        }
        else
        {
            MessageBox.Show("Invalid time format");
        }
    }

    /// <summary>
    ///     This method saves the bed time to the database.
    /// </summary>
    private void BedTimeSave()
    {
        TimeOnly timeOnly;
        var InputInCorrectFormat = TimeOnly.TryParseExact(BedTime, "H:mm", out timeOnly);

        if (InputInCorrectFormat)
        {
            _userRepository.UpdateUsersBedTime(_userId, timeOnly);
            MessageBox.Show("Bed time saved!");
        }
        else
        {
            MessageBox.Show("Invalid format!");
        }
    }

    /// <summary>
    ///     This method saves the wake up time to the database.
    /// </summary>
    private void SaveWakeUpTime()
    {
        TimeOnly timeOnly;
        var InputInCorrectFormat = TimeOnly.TryParseExact(WakeUpTime, "H:mm", out timeOnly);

        if (InputInCorrectFormat)
        {
            _userRepository.UpdateUsersWakeUpTime(_userId, timeOnly);
            MessageBox.Show("Wake up time saved!");
        }
        else
        {
            MessageBox.Show("Invalid format!");
        }
    }
}