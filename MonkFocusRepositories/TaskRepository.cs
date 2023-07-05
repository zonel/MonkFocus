using MonkFocusDataAccess;
using MonkFocusModels;
using MonkFocusRepositories.Interfaces;

namespace MonkFocusRepositories;

/// <summary>
///     This class is used to perform CRUD operations on the Task table in the database.
/// </summary>
public class TaskRepository : ITaskRepository
{
    #region DI Fields

    private readonly IMonkFocusDbContext _context;

    #endregion

    public TaskRepository(IMonkFocusDbContext context)
    {
        _context = context;
    }

    /// <summary>
    ///     This method adds a new task to the database.
    /// </summary>
    /// <param name="userTask">defined userTask data</param>
    /// <exception cref="System.ArgumentNullException">UserTask was null</exception>
    public void AddTask(UserTask userTask)
    {
        if (userTask is null) throw new ArgumentNullException(nameof(userTask));

        _context.Tasks.Add(userTask);
        _context.SaveChanges();
    }

    /// <summary>
    ///     This method returns a task from the database based on the TaskId.
    /// </summary>
    /// <param name="userTask">defined userTask data</param>
    /// <exception cref="System.ArgumentNullException">UserTask was null</exception>
    public void UpdateTask(UserTask userTask)
    {
        if (userTask is null) throw new ArgumentNullException(nameof(userTask));

        _context.Tasks.Update(userTask);
        _context.SaveChanges();
    }

    /// <summary>
    ///     This method deletes a task from the database based on the TaskId.
    /// </summary>
    /// <param name="TaskId">TaskID of task to delete</param>
    public void DeleteTaskById(int TaskId)
    {
        var taskToDelete = _context.Tasks.FirstOrDefault(t => t.TaskId == TaskId);

        if (taskToDelete != null)
        {
            _context.Tasks.Remove(taskToDelete);
            _context.SaveChanges();
        }
    }

    /// <summary>
    ///     This method returns all tasks from the database for a given user.
    /// </summary>
    /// <param name="userId">given user</param>
    /// <returns>Collection of type UserTask</returns>
    public IEnumerable<UserTask> GetAllTasksForUser(int userId)
    {
        var userTasks = _context.Tasks.Where(t => t.UserId == userId).ToList();

        if (userTasks != null) return userTasks;

        return null;
    }

    /// <summary>
    ///     This method returns 10 most recent tasks from the database for a given user that are not completed.
    /// </summary>
    /// <param name="userId">given user</param>
    /// <returns>10 most recent tasks from the database in IEnumerable collection</returns>
    public IEnumerable<UserTask> GetTop10NotCompletedTasksForUser(int userId)
    {
        var userTasks = _context.Tasks
            .Where(t => t.UserId == userId)
            .Where(t => t.StatusId == 1 || t.StatusId == 3 || t.StatusId == 2) //TODO - remove statusId == 2 from here
            .Take(10)
            .ToList();

        if (userTasks != null) return userTasks;

        return null;
    }
}