using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MonkFocusApp.Commands;
using MonkFocusApp.DTO;
using MonkFocusApp.Windows;
using MonkFocusDataAccess;
using MonkFocusModels;
using MonkFocusRepositories;

namespace MonkFocusApp.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        #region Repositories
        private UserRepository _userRepository;
        private TaskRepository _taskRepository;
        private WorkSessionRepository _WorkSessionRepository;
        private MonkFocusRepository _monkRepository;
        #endregion
        #region Properties
        private readonly int _userId;
        private User _user;
        private string _welcome;
        private string _clock;
        private string _timeUntilBedtime;
        private readonly Quote _quote;
        private string _wakeuptime;
        private string _bedtime;
        private TimeOnly _remainingWorkTime;
        private bool _timerIsRunning = false;
        private DateTime _startTime;
        private string _buttonContent;
        private SolidColorBrush _buttonColor;
        private string _worksessionclock;
        private TimeSpan duration = new TimeSpan(0, 0, 0);
        #endregion
        #region Properties for Tasks
        private readonly IEnumerable<UserTask> _tasks;
        private  ObservableCollection<UserTaskDTO> _userTasks;
        #endregion
        #region Properties for LatestSessions
        private readonly IEnumerable<WorkSession> _userLatestSessions;
        private ObservableCollection<LatestSessionsDTO> _userLatestSessionsDTOd;
        #endregion
        #region Properties for Leaderboard
        private readonly IEnumerable<User> _Leaderboard;
        private ObservableCollection<LeaderboardDTO> _LeaderboardDTOd;
        #endregion
        #region Commands
        public ICommand WorkSessionCommand { get; }
        public ICommand TaskManagerCommand { get; }
        public ICommand WebsitesBlockerCommand { get; }
        public ICommand SettingsCommand { get; }
        #endregion

        /// <summary>
        /// DashboardView is a main window of my app that is used to display all the information and interactions with user.
        /// </summary>
        /// <param name="userId">userId of the user for which .AuthenticateUser in LoginView returned true.</param>
        /// <param name="context">Context of the DB created in LoginView</param>
        public DashboardViewModel(int userId, MonkFocusDbContext context)
        {
            #region Repositories
            _taskRepository = new TaskRepository(context);
            _userRepository = new UserRepository(context);
            _monkRepository = new MonkFocusRepository(context);
            _WorkSessionRepository = new WorkSessionRepository(context);
            #endregion
            #region Field Assigning
            _userId = userId; // our userId retrieved from the login screen
            _user = _userRepository.GetUserById(_userId); //getting all info about the user from DB
            _wakeuptime = _user.WakeUpTime.ToString("HH:mm");
            _bedtime = _user.BedTime.ToString("HH:mm");
            _buttonContent = "START YOUR WORK SESSION";
            WorkSessionClock = "00 : 00 : 00";
            ButtonColor = new SolidColorBrush(Color.FromArgb(255,60,155,176));
            WorkSessionCommand = new RelayCommand(WorkSessionButtonCommand);
            TaskManagerCommand = new RelayCommand(TaskManagerWindowCommand);
            WebsitesBlockerCommand = new RelayCommand(WebsitesBlockerWindowCommand);
            SettingsCommand = new RelayCommand(SettingsWindowCommand);
            #endregion
            #region Tasks
            _tasks = _taskRepository.GetTop10NotCompletedTasksForUser(_userId);
            IEnumerable<UserTaskDTO> userTaskDTOs = _tasks.Select(t => new UserTaskDTO(t));
            _userTasks = new ObservableCollection<UserTaskDTO>(userTaskDTOs);
            #endregion
            #region LatestSessions
            _userLatestSessions = _userRepository.GetTop3LatestSessionsForUser(_userId);
            IEnumerable<LatestSessionsDTO> latestSessionsDTOs = _userLatestSessions.Select(s => new LatestSessionsDTO(s));
            _userLatestSessionsDTOd = new ObservableCollection<LatestSessionsDTO>(latestSessionsDTOs);
            #endregion
            #region Leaderboard
            _Leaderboard = _monkRepository.GetTop3Leaderboard();
            IEnumerable<LeaderboardDTO> LeaderboardDto = _Leaderboard.Select(s => new LeaderboardDTO(s));
            _LeaderboardDTOd = new ObservableCollection<LeaderboardDTO>(LeaderboardDto);
            #endregion
            #region Quote
            _quote = _monkRepository.GetRandomQuote();
            #endregion
            #region RemainingWorkTime
            _remainingWorkTime = _userRepository.GetUsersRemainingWorkTimeForToday(_userId);
            #endregion
            #region Clock
            ClockInitialize();
            #endregion
        }

        #region Commands
        private void WorkSessionButtonCommand()
        {
            if (_timerIsRunning)
            {
                //stop the clock and save the session if session is >1 minute.
                if (duration > new TimeSpan(0,1,0))
                {
                    _WorkSessionRepository.AddWorkSession(new WorkSession()
                    {
                        Duration = duration,
                        StartTime = _startTime,
                        EndTime = DateTime.Now,
                        UserId = _userId,
                        Points = Math.Floor(duration.TotalMinutes).ToString("####")
                    });
                    MessageBox.Show("You have earned " + Math.Floor(duration.TotalMinutes).ToString("####") + " points!");
                    LatestSesssions = new ObservableCollection<LatestSessionsDTO>(_userLatestSessions.Select(s => new LatestSessionsDTO(s)));
                }
                else
                {
                    MessageBox.Show("Session was shorter than 1 minutes. No points for you :(");
                }
                ButtonContent = "START YOUR WORK SESSION";
                ButtonColor = new SolidColorBrush(Color.FromArgb(255,60,155,176));
            }
            else
            {
                //start the clock.
                _startTime = DateTime.Now;
                ButtonContent = "END YOUR WORK SESSION";
                ButtonColor = new SolidColorBrush(Color.FromArgb(255,33,84,95));
            }
            _timerIsRunning = !_timerIsRunning;
            WorkSessionClock = "00 : 00 : 00";
        }

        private void TaskManagerWindowCommand()
        {
            TaskManagerWindow taskManagerWindow = new TaskManagerWindow(_userId);
            taskManagerWindow.Show();
        } 

        private void WebsitesBlockerWindowCommand()
        {
            WebsiteBlockerWindow websiteblockerwindow = new WebsiteBlockerWindow(_userId);
            websiteblockerwindow.Show();
        }

        private void SettingsWindowCommand()
        {
            SettingsWindow settingsWindow = new SettingsWindow(_userId);
            settingsWindow.Show();
        }
        #endregion
        #region Clock Initialize & Clock Clicks for every service
        private void ClockInitialize()
        {
            System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();

            Timer.Tick += new EventHandler(Timer_Click);
            Timer.Tick += new EventHandler(Bedtime_Click);
            Timer.Tick += new EventHandler(WorkSessionTime_Click);

            Timer.Interval = new TimeSpan(0, 0, 1);

            Timer.Start();
        }
        private void Bedtime_Click(object? sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            var BedTime = _user.BedTime;
            DateTime bedtime = DateTime.Today.Add(_user.BedTime.ToTimeSpan());

            if (bedtime < now) bedtime = bedtime.AddDays(1);

            TimeSpan timeUntilBedtime = bedtime - now;
            TimeUntilBedtime = string.Format("{0} hours {1} minutes", timeUntilBedtime.Hours, timeUntilBedtime.Minutes);
        }
        private void Timer_Click(object sender, EventArgs e)
        {
            DateTime d;
            d = DateTime.Now;
            Clock = string.Format("{0} : {1} : {2}", d.Hour.ToString("00"), d.Minute.ToString("00"), d.Second.ToString("00"));
        }
        private void WorkSessionTime_Click(object sender, EventArgs e)
        {
            if (_timerIsRunning)
            {
                WorkSessionClock = string.Format("{0} : {1} : {2}", duration.Hours.ToString("00"), duration.Minutes.ToString("00"), duration.Seconds.ToString("00"));
                duration = duration.Add(new TimeSpan(0, 0, 1));
            }
        }
        #endregion
        #region Public Fields
        public string WorkSessionClock
        {
            get { return _worksessionclock; }
            set
            {
                if (_worksessionclock != value)
                {
                    _worksessionclock = value;
                    OnPropertyChanged(nameof(WorkSessionClock));
                }
            }
        }
        public ObservableCollection<UserTaskDTO> Tasks
        {
            get { return _userTasks; }
            set
            {
                _userTasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }
        public ObservableCollection<LatestSessionsDTO> LatestSesssions
        {
            get { return _userLatestSessionsDTOd; }
            set
            {
                _userLatestSessionsDTOd = value;
                OnPropertyChanged(nameof(Tasks));
                OnPropertyChanged(nameof(WorkSessionClock));
            }
        }
        public ObservableCollection<LeaderboardDTO> Leaderboard
        {
            get { return _LeaderboardDTOd; }
            set
            {
                _LeaderboardDTOd = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }
        public string WelcomeMessage
        {
            get { return $"Hi, {_user.Name}"; }
            set
            {
                if (_welcome != value)
                {
                    _welcome = value;
                    OnPropertyChanged(nameof(WelcomeMessage));
                }
            }
        }
        public string Clock
        {
            get { return _clock; }
            set
            {
                if (_clock != value)
                {
                    _clock = value;
                    OnPropertyChanged(nameof(Clock));
                }
            }
        }
        public string TimeUntilBedtime 
        {
            get { return _timeUntilBedtime; }
            set
            {
                if (_timeUntilBedtime != value)
                {
                    _timeUntilBedtime = value;
                    OnPropertyChanged(nameof(TimeUntilBedtime));
                }
            }
        }
        public string BedTime 
        {
            get { return "💤 = "+_bedtime; }
            set
            {
                if (_bedtime != value)
                {
                    _bedtime = value;
                    OnPropertyChanged(nameof(BedTime));
                }
            }
        }
        public string WakeupTime 
        {
            get { return "🌤️ = "+_wakeuptime; }
            set
            {
                if (_wakeuptime != value)
                {
                    _wakeuptime = value;
                    OnPropertyChanged(nameof(WakeupTime));
                }
            }
        }
        public string QuoteText
        {
            get { return _quote.FullQuote; }
            set
            {
                if (_quote.FullQuote != value)
                {
                    _quote.FullQuote = value;
                    OnPropertyChanged(nameof(QuoteText));
                }
            }
        }
        public string QuoteAuthor
        {
            get { return _quote.Author; }
            set
            {
                if (_quote.Author != value)
                {
                    _quote.Author = value;
                    OnPropertyChanged(nameof(QuoteAuthor));
                }
            }
        }
        public string RemainingWorkTime
        {
            get { return _remainingWorkTime.Hour+" hours "+_remainingWorkTime.Minute+" minutes"; }
            set
            {
                if (_remainingWorkTime.Hour+" hours "+_remainingWorkTime.Minute+" minutes" != value)
                {
                    //_remainingWorkTime.ToString() = value;
                    OnPropertyChanged(nameof(RemainingWorkTime));
                }
            }
        }
        public SolidColorBrush ButtonColor
        {
            get { return _buttonColor; }
            set
            {
                _buttonColor = value;
                OnPropertyChanged(nameof(ButtonColor));
            }
        }
        public string ButtonContent
        {
            get { return _buttonContent; }
            set
            {
                _buttonContent = value;
                OnPropertyChanged(nameof(ButtonContent));
            }
        }
        #endregion
    }
}
