using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Accessibility;
using MonkFocusApp.DTO;
using MonkFocusDataAccess;
using MonkFocusModels;
using MonkFocusRepositories;
using MonkFocusRepositories.Interfaces;

namespace MonkFocusApp.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private readonly int _userId;
        private UserRepository _userRepository;
        private TaskRepository _taskRepository;
        private MonkFocusRepository _monkRepository;

        private User _user;
        private string _welcome;
        private string _clock;
        private string _timeUntilBedtime;

        private readonly IEnumerable<UserTask> _tasks;
        private  ObservableCollection<UserTaskDTO> _userTasks;

        private readonly IEnumerable<WorkSession> _userLatestSessions;
        private ObservableCollection<LatestSessionsDTO> _userLatestSessionsDTOd;

        private readonly IEnumerable<User> _Leaderboard;
        private ObservableCollection<LeaderboardDTO> _LeaderboardDTOd;
        
        private readonly Quote _quote;
        private string _wakeuptime;
        private string _bedtime;

        public DashboardViewModel(int userId, MonkFocusDbContext context)
        {
            #region Repositories
            _taskRepository = new TaskRepository(context);
            _userRepository = new UserRepository(context);
            _monkRepository = new MonkFocusRepository(context);
            #endregion
            _userId = userId; // our userId retrieved from the login screen
            _user = _userRepository.GetUserById(_userId); //getting all info about the user from DB
            _wakeuptime = _user.WakeUpTime.ToString("HH:mm");
            _bedtime = _user.BedTime.ToString("HH:mm");

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

            #region Clock
            ClockInitialize();
            #endregion
        }

        #region RealTime Clock
        private void ClockInitialize()
        {
            System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();

            Timer.Tick += new EventHandler(Timer_Click);
            Timer.Tick += new EventHandler(Bedtime_Click);

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

        public void Timer_Click(object sender, EventArgs e)
        {
            DateTime d;
            d = DateTime.Now;
            Clock = string.Format("{0}:{1}:{2}", d.Hour.ToString("00"), d.Minute.ToString("00"), d.Second.ToString("00"));
        }
        #endregion

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

    }
}
