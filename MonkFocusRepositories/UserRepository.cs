using MonkFocusDataAccess;
using MonkFocusModels;
using MonkFocusRepositories.Interfaces;
using System;

namespace MonkFocusRepositories
{
    public class UserRepository : IUserRepository
    {
        #region DI Fields
        private readonly MonkFocusDbContext _context;
        #endregion

        public UserRepository(MonkFocusDbContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            if(user is null) throw new System.ArgumentNullException(nameof(user));

            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void DeleteUserById(int userId)
        {
            var userToDelete = _context.Users.FirstOrDefault(u => u.UserId == userId);

            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
            }
        }
        public User GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == userId) ?? throw new NullReferenceException();
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username) ?? throw new NullReferenceException();
        }

        public bool AuthenticateUser(string username, string password) //TODO: strip input from whitespaces
        {
            if (username == "" || password == "") return false;

            var userToValidate = _context.Users.FirstOrDefault(u => u.Username == username);

            if (userToValidate is null) return false;

            return userToValidate.Password == password;
        }

        public IEnumerable<WorkSession> GetTop3LatestSessionsForUser(int userId)
        {
            return _context.WorkSessions
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.StartTime)
                .Take(3)
                .ToList();
        }

        /// <summary>
        /// Returns remaining work time left today for a given user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>TimeOnly data type in format hh:mm:ss</returns>
        public TimeOnly GetUsersRemainingWorkTimeForToday(int userId)
        {
            var todaysWorkSessions = _context.WorkSessions
                .Where(t => t.UserId == userId && t.StartTime.Date == DateTime.Today)
                .Select(ws => ws.Duration)
                .ToList();

            todaysWorkSessions.Add(new TimeSpan(0, 0, 0)); //add a zero timespan to the list to avoid nullreferenceexception

            TimeSpan totalWorkTimeToday = new TimeSpan();
            foreach (var session in todaysWorkSessions)
            {
                totalWorkTimeToday += session;
            }

            var getUsersWorkTimeGoal = _context.Users.FirstOrDefault(u => u.UserId == userId)?.WorkTimeGoal ?? new TimeSpan(5,0,0);

            TimeSpan remainingWorkTime = getUsersWorkTimeGoal - totalWorkTimeToday;

            return new TimeOnly(remainingWorkTime.Hours, remainingWorkTime.Minutes, remainingWorkTime.Seconds);
        }
    }
}