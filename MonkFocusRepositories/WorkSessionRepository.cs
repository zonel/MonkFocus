using MonkFocusDataAccess;
using MonkFocusModels;
using MonkFocusRepositories.Interfaces;

namespace MonkFocusRepositories;

/// <summary>
///     This method is used to perform CRUD operations on the WorkSession table in the database.
/// </summary>
public class WorkSessionRepository : IWorkSessionRepository
{
    #region DI Fields

    private readonly IMonkFocusDbContext _context;

    #endregion

    public WorkSessionRepository(IMonkFocusDbContext context)
    {
        _context = context;
    }

    /// <summary>
    ///     This method adds a new work session to the database.
    /// </summary>
    /// <param name="workSession">given work session</param>
    /// <exception cref="System.ArgumentNullException">Work session you've provided was null</exception>
    public void AddWorkSession(WorkSession workSession)
    {
        if (workSession is null) throw new ArgumentNullException(nameof(workSession));

        _context.WorkSessions.Add(workSession);
        _context.SaveChanges();
    }

    /// <summary>
    ///     This method deletes a new work session to the database.
    /// </summary>
    /// <param name="workSession">given work session</param>
    public void DeleteWorkSessionById(int workSessionId)
    {
        var WorkSessionToDelete = _context.WorkSessions
            .FirstOrDefault(w => w.WorkSessionId == workSessionId);

        if (WorkSessionToDelete != null)
        {
            _context.WorkSessions.Remove(WorkSessionToDelete);
            _context.SaveChanges();
        }
    }

    /// <summary>
    ///     This method gets all work sessions from the database.
    /// </summary>
    /// <param name="workSession">given work session</param>
    public IEnumerable<WorkSession> GetAllWorkSessionsForUser(int userId)
    {
        var usersWorkSessions = _context.WorkSessions
            .Where(w => w.UserId == userId)
            .ToList();

        if (usersWorkSessions is null) return null;

        return usersWorkSessions;
    }
}