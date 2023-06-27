using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MonkFocusApp.Commands;
using MonkFocusDataAccess;
using MonkFocusModels;
using MonkFocusRepositories;

namespace MonkFocusApp.ViewModels
{
    class SettingsViewModel : BaseViewModel
    {
        private string _wakeuptime;
        private string _bedtime;
        private string _worktimegoal;

        private readonly UserRepository _userRepository;
        private readonly int _userId;

        public ICommand WakeUpTimeSaveCommand { get; }
        public ICommand BedTimeSaveCommand { get; }
        public ICommand WorkTimeGoalSaveCommand { get; }
        public string WakeUpTime
        {
            get { return _wakeuptime; }
            set
            {
                if (_wakeuptime != value)
                {
                    _wakeuptime = value;
                    OnPropertyChanged(nameof(WakeUpTime));
                }
            }
        }
        public string BedTime
        {
            get { return _bedtime; }
            set
            {
                if (_bedtime != value)
                {
                    _bedtime = value;
                    OnPropertyChanged(nameof(BedTime));
                }
            }
        }
        public string WorkTimeGoal
        {
            get { return _worktimegoal; }
            set
            {
                if (_worktimegoal != value)
                {
                    _worktimegoal = value;
                    OnPropertyChanged(nameof(WorkTimeGoal));
                }
            }
        }


        public SettingsViewModel(int userId)
        {
            _userId = userId;
            _userRepository = new UserRepository(new MonkFocusDbContext());

            WakeUpTimeSaveCommand = new RelayCommand(SaveWakeUpTime);
            BedTimeSaveCommand = new RelayCommand(BedTimeSave);
            WorkTimeGoalSaveCommand = new RelayCommand(WorkTimeSave);
        }
        private void WorkTimeSave()
        {
            TimeSpan timeOnly;
            string pattern = @"^(0?[0-9]|1[0-9]|2[0-3]):([0-5][0-9])$";
            Match match = Regex.Match(WorkTimeGoal, pattern);

            if (match.Success)
            {
                int hours = int.Parse(match.Groups[1].Value);
                int minutes = int.Parse(match.Groups[2].Value);

                TimeSpan timeSpan = new TimeSpan(hours, minutes, 0);

                _userRepository.UpdateUsersWorkTimeGoal(_userId, timeSpan);
                MessageBox.Show("Work Time Goal saved!");
            }
            else
            {
                Console.WriteLine("Invalid time format");
            }
        }
        private void BedTimeSave()
        {
            TimeOnly timeOnly;
            bool InputInCorrectFormat = TimeOnly.TryParseExact(BedTime, "H:mm", out timeOnly);

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
        private void SaveWakeUpTime()
        {
            TimeOnly timeOnly;
            bool InputInCorrectFormat = TimeOnly.TryParseExact(WakeUpTime, "H:mm", out timeOnly);

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
}
