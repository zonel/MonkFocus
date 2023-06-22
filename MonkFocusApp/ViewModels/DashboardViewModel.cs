using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        private User _user;
        private string _welcome;
        private string _clock;


        public DashboardViewModel(int userId, MonkFocusDbContext context)
        {
            _userId = userId;
            _userRepository = new UserRepository(context);
            _user = _userRepository.GetUserById(_userId);
            ClockInitialize();


        }

        #region RealTime Clock
        private void ClockInitialize()
        {
            System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();

            Timer.Tick += new EventHandler(Timer_Click);

            Timer.Interval = new TimeSpan(0, 0, 1);

            Timer.Start();
        }
        public void Timer_Click(object sender, EventArgs e)
        {
            DateTime d;
            d = DateTime.Now;
            Clock = string.Format("{0}:{1}:{2}", d.Hour.ToString("00"), d.Minute.ToString("00"), d.Second.ToString("00"));
        }
        #endregion

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



        
    }
}
