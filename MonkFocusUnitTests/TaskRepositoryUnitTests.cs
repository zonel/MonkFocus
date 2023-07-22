using Microsoft.EntityFrameworkCore;
using MonkFocusDataAccess;
using MonkFocusModels;
using MonkFocusRepositories;

namespace MonkFocusUnitTests;

[TestClass]
public class TaskRepositoryUnitTests
{
    private MonkFocusMockDbContext _dbContext;

    private DbContextOptions<MonkFocusMockDbContext> _options;
    private TaskRepository _taskrepository;

    [TestInitialize]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<MonkFocusMockDbContext>()
            .UseInMemoryDatabase("UnitTestDatabase")
            .Options;
        _dbContext = new MonkFocusMockDbContext(_options);
        SeedTestData();
        _taskrepository = new TaskRepository(_dbContext);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _dbContext.Dispose();
        _taskrepository = null;
    }

    [TestMethod]
    public void AddTask_CheckIfTaskExists()
    {
        var usertask = new UserTask
        {
            UserId = 1,
            TaskName = "THIS IS A TEST TASK",
            PriorityId = 3,
            StatusId = 1
        };
        _taskrepository.AddTask(usertask);

        var checkForTestTask = _dbContext.Tasks.Where(t => t.TaskName == "THIS IS A TEST TASK").FirstOrDefault();
        Assert.IsNotNull(checkForTestTask);
    }

    [TestMethod]
    public void UpdateTask_ValidUserTask_TaskUpdatedInDatabase()
    {
        var userTask = new UserTask
        {
            UserId = 1,
            TaskName = "Updated Task",
            PriorityId = 2,
            StatusId = 2
        };

        _taskrepository.UpdateTask(userTask);

        var updatedTask = _dbContext.Tasks.FirstOrDefault(t => t.TaskName == "Updated Task");
        Assert.IsNotNull(updatedTask);
        Assert.AreEqual("Updated Task", updatedTask.TaskName);
        Assert.AreEqual(2, updatedTask.PriorityId);
        Assert.AreEqual(2, updatedTask.StatusId);
    }

    [TestMethod]
    public void DeleteTaskById_ExistingTaskId_TaskDeletedFromDatabase()
    {
        var taskIdToDelete = 1;

        _taskrepository.DeleteTaskById(taskIdToDelete);

        var deletedTask = _dbContext.Tasks.FirstOrDefault(t => t.TaskId == taskIdToDelete);
        Assert.IsNull(deletedTask);
    }

    [TestMethod]
    public void GetAllTasksForUser_ExistingUserId_ReturnsUserTasks()
    {
        var userId = 1;

        var tasks = _taskrepository.GetAllTasksForUser(userId);

        Assert.IsNotNull(tasks);
        Assert.AreEqual(8, tasks.Count());

    }

    [TestMethod]
    public void GetTop10NotCompletedTasksForUser_ExistingUserId_ReturnsTop10Tasks()
    {
        var userId = 2;

        var tasks = _taskrepository.GetTop10NotCompletedTasksForUser(userId);

        Assert.IsNotNull(tasks);
        Assert.AreEqual(3, tasks.Count());
    }

    private void SeedTestData()
    {
        if (!_dbContext.Tasks.Any())
            _dbContext.Tasks.AddRange(
                new UserTask
                {
                    UserId = 1,
                    TaskName = "Go to Gym",
                    PriorityId = 3,
                    StatusId = 1
                },
                new UserTask
                {
                    UserId = 1,
                    TaskName = "Finish today's work",
                    PriorityId = 3,
                    StatusId = 2
                },
                new UserTask
                {
                    UserId = 1,
                    TaskName = "Fix Bug #123",
                    PriorityId = 3,
                    StatusId = 2
                },
                new UserTask
                {
                    UserId = 1,
                    TaskName = "Morning Meditation",
                    PriorityId = 1,
                    StatusId = 2
                },
                new UserTask
                {
                    UserId = 1,
                    TaskName = "Take Supplements",
                    PriorityId = 1,
                    StatusId = 2
                },
                new UserTask
                {
                    UserId = 1,
                    TaskName = "Daily Journal Entry",
                    PriorityId = 2,
                    StatusId = 2
                },
                new UserTask
                {
                    UserId = 1,
                    TaskName = "Groceries",
                    PriorityId = 1,
                    StatusId = 2
                },
                new UserTask
                {
                    UserId = 2,
                    TaskName = "Go for a run",
                    PriorityId = 1,
                    StatusId = 1
                },
                new UserTask
                {
                    UserId = 2,
                    TaskName = "Read 10 pages of a book",
                    PriorityId = 3,
                    StatusId = 1
                },
                new UserTask
                {
                    UserId = 2,
                    TaskName = "Do Stretching",
                    PriorityId = 3,
                    StatusId = 1
                }
            );
        _dbContext.SaveChanges();
    }
}