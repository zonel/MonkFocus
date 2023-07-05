using Microsoft.EntityFrameworkCore;
using MonkFocusDataAccess;
using MonkFocusModels;
using MonkFocusRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkFocusUnitTests
{
    [TestClass]
    public class TaskRepositoryUnitTests
    {
        private MonkFocusMockDbContext _dbContext;
        private TaskRepository _taskrepository;

        private DbContextOptions<MonkFocusMockDbContext> _options;
        [TestInitialize]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<MonkFocusMockDbContext>()
                .UseInMemoryDatabase(databaseName: "UnitTestDatabase")
                .Options;
            _dbContext = new MonkFocusMockDbContext(_options);
            SeedTestData();
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
            _taskrepository = new TaskRepository(_dbContext);
            var usertask = new UserTask
            {
                UserId = 1,
                TaskName = "THIS IS A TEST TASK",
                PriorityId = 3,
                StatusId = 1,
            };
            _taskrepository.AddTask(usertask);

            var checkForTestTask = _dbContext.Tasks.Where(t => t.TaskName == "THIS IS A TEST TASK").FirstOrDefault();
            Assert.IsNotNull(checkForTestTask);
        }

        [TestMethod]
        public void GetRandomQuote_ReturnsRandomQuote()
        {
            //_taskrepository = new MonkFocusRepository(_dbContext);
            //var result = _taskrepository.GetRandomQuote();

            //Assert.IsNotNull(result);
        }

        private void SeedTestData()
        {
            if (!_dbContext.Tasks.Any())
            {
                _dbContext.Tasks.AddRange(
                    new UserTask
                    {
                        UserId = 1,
                        TaskName = "Go to Gym",
                        PriorityId = 3,
                        StatusId = 1,
                    },
                    new UserTask
                    {
                        UserId = 1,
                        TaskName = "Finish today's work",
                        PriorityId = 3,
                        StatusId = 2,
                    },
                    new UserTask
                    {
                        UserId = 1,
                        TaskName = "Fix Bug #123",
                        PriorityId = 3,
                        StatusId = 2,
                    },
                    new UserTask
                    {
                        UserId = 1,
                        TaskName = "Morning Meditation",
                        PriorityId = 1,
                        StatusId = 2,
                    },
                    new UserTask
                    {
                        UserId = 1,
                        TaskName = "Take Supplements",
                        PriorityId = 1,
                        StatusId = 2,
                    },
                    new UserTask
                    {
                        UserId = 1,
                        TaskName = "Daily Journal Entry",
                        PriorityId = 2,
                        StatusId = 2,
                    },
                    new UserTask
                    {
                        UserId = 1,
                        TaskName = "Groceries",
                        PriorityId = 1,
                        StatusId = 2,
                    },
                    new UserTask
                    {
                        UserId = 2,
                        TaskName = "Go for a run",
                        PriorityId = 1,
                        StatusId = 1,
                    },
                    new UserTask
                    {
                        UserId = 2,
                        TaskName = "Read 10 pages of a book",
                        PriorityId = 3,
                        StatusId = 1,
                    },
                    new UserTask
                    {
                        UserId = 2,
                        TaskName = "Do Stretching",
                        PriorityId = 3,
                        StatusId = 1,
                    }
                    );
            }
            _dbContext.SaveChanges();
        }
    }
}
