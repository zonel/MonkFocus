using MonkFocusDataAccess;
using MonkFocusModels;
using MonkFocusRepositories.Interfaces;

namespace MonkFocusRepositories;

/// <summary>
///     This class is used to perform CRUD operations on the User table in the database.
/// </summary>
public class UserRepository : IUserRepository
{
    #region DI Fields

    private readonly IMonkFocusDbContext _context;

    #endregion

    public UserRepository(IMonkFocusDbContext context)
    {
        _context = context;
    }

    /// <summary>
    ///     This method adds a new user to the database.
    /// </summary>
    /// <param name="user">given user</param>
    /// <exception cref="System.ArgumentNullException">User is null</exception>
    public void AddUser(User user)
    {
        if (user is null) throw new ArgumentNullException(nameof(user));

        _context.Users.Add(user);
        _context.SaveChanges();
    }

    /// <summary>
    ///     This method deletes a user from the database based on the UserId.
    /// </summary>
    /// <param name="userId">given user</param>
    public void DeleteUserById(int userId)
    {
        var userToDelete = _context.Users.FirstOrDefault(u => u.UserId == userId);

        if (userToDelete != null)
        {
            _context.Users.Remove(userToDelete);
            _context.SaveChanges();
        }
    }

    /// <summary>
    ///     This method returns a user from the database based on the UserId.
    /// </summary>
    /// <param name="userId">given userId</param>
    /// <returns>object of type User</returns>
    /// <exception cref="NullReferenceException">This user was not found.</exception>
    public User GetUserById(int userId)
    {
        return _context.Users.FirstOrDefault(u => u.UserId == userId) ?? throw new NullReferenceException();
    }

    /// <summary>
    ///     This method authenticates a user based on the username and password.
    /// </summary>
    /// <param name="username">user's username</param>
    /// <param name="password">user's password</param>
    /// <returns>True if those credentials matched</returns>
    public bool AuthenticateUser(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return false;

        var userToValidate = _context.Users.FirstOrDefault(u => u.Username == username.Trim());

        if (userToValidate is null) return false;

        return userToValidate.Password == password;
    }

    /// <summary>
    ///     This method returns a user from the database based on the Username.
    /// </summary>
    /// <param name="username">given username</param>
    /// <returns>object of type User</returns>
    /// <exception cref="NullReferenceException">This user was not found.</exception>
    public User GetUserByUsername(string username)
    {
        return _context.Users.FirstOrDefault(u => u.Username == username) ?? throw new NullReferenceException();
    }

    /// <summary>
    ///     This method checks if the user exists in the database.
    /// </summary>
    /// <param name="username">given user</param>
    /// <returns>True if user is in Database, false if is not found</returns>
    public bool CheckifUserExists(string username)
    {
        return _context.Users.FirstOrDefault(u => u.Username == username) is not null;
    }


    /// <summary>
    ///     This method returns the top 3 latest work sessions for a given user.
    /// </summary>
    /// <param name="userId">give userId</param>
    /// <returns>Collection of 3 most recent sessions for user</returns>
    public IEnumerable<WorkSession> GetTop3LatestSessionsForUser(int userId)
    {
        return _context.WorkSessions
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.StartTime)
            .Take(3)
            .ToList();
    }

    /// <summary>
    ///     Returns remaining work time left today for a given user.
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

        var totalWorkTimeToday = new TimeSpan();
        foreach (var session in todaysWorkSessions) totalWorkTimeToday += session;

        var getUsersWorkTimeGoal = _context.Users.FirstOrDefault(u => u.UserId == userId)?.WorkTimeGoal ??
                                   new TimeSpan(5, 0, 0);

        var remainingWorkTime = getUsersWorkTimeGoal - totalWorkTimeToday;
        if (remainingWorkTime < TimeSpan.Zero) remainingWorkTime = TimeSpan.Zero;

        return new TimeOnly(remainingWorkTime.Hours, remainingWorkTime.Minutes, remainingWorkTime.Seconds);
    }

    /// <summary>
    ///     This method updates users wake up time.
    /// </summary>
    /// <param name="userId">Given userId</param>
    /// <param name="wakeUpTime">New wake up time provided for user</param>
    public void UpdateUsersWakeUpTime(int userId, TimeOnly wakeUpTime)
    {
        var userToUpdate = _context.Users.FirstOrDefault(u => u.UserId == userId);
        if (userToUpdate != null)
        {
            userToUpdate.WakeUpTime = wakeUpTime;
            _context.SaveChanges();
        }
    }


    /// <summary>
    ///     This method updates users bed time.
    /// </summary>
    /// <param name="userId">given userId</param>
    /// <param name="bedTime">New bed time provided for user</param>
    public void UpdateUsersBedTime(int userId, TimeOnly bedTime)
    {
        var userToUpdate = _context.Users.FirstOrDefault(u => u.UserId == userId);
        if (userToUpdate != null)
        {
            userToUpdate.BedTime = bedTime;
            _context.SaveChanges();
        }
    }

    /// <summary>
    ///     This method updates users work time goal.
    /// </summary>
    /// <param name="userId">given userId</param>
    /// <param name="workTime">New work time goal provided for user</param>
    public void UpdateUsersWorkTimeGoal(int userId, TimeSpan workTime)
    {
        var userToUpdate = _context.Users.FirstOrDefault(u => u.UserId == userId);
        if (userToUpdate != null)
        {
            userToUpdate.WorkTimeGoal = workTime;
            _context.SaveChanges();
        }
    }
}