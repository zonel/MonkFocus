using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkFocusDataAccess;
using MonkFocusModels;
using MonkFocusRepositories;
using System;
using System.Linq;

namespace MonkFocusUnitTests
{
    [TestClass]
    public class WorkSessionRepositoryUnitTests
    {
        private WorkSessionRepository _workSessionRepository;
        private MonkFocusMockDbContext _dbContext;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MonkFocusMockDbContext>()
                .UseInMemoryDatabase(databaseName: "UnitTestDatabase")
                .Options;
            _dbContext = new MonkFocusMockDbContext(options);

            _workSessionRepository = new WorkSessionRepository(_dbContext);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _dbContext.Dispose();
            _workSessionRepository = null;
        }

        [TestMethod]
        public void AddWorkSession_ValidWorkSession_WorkSessionAddedToDatabase()
        {
            var workSession = new WorkSession
            {
                WorkSessionId = 1,
                UserId = 1,
                StartTime = DateTime.Now,
                Points = "123",
            };

            _workSessionRepository.AddWorkSession(workSession);

            var checkForWorkSession = _dbContext.WorkSessions.FirstOrDefault(w => w.WorkSessionId == workSession.WorkSessionId);
            Assert.IsNotNull(checkForWorkSession);
        }

        [TestMethod]
        public void DeleteWorkSessionById_ExistingWorkSessionId_WorkSessionDeletedFromDatabase()
        {
            var workSessionIdToDelete = 1;

            _workSessionRepository.DeleteWorkSessionById(workSessionIdToDelete);

            var deletedWorkSession = _dbContext.WorkSessions.FirstOrDefault(w => w.WorkSessionId == workSessionIdToDelete);
            Assert.IsNull(deletedWorkSession);
        }

        [TestMethod]
        public void GetAllWorkSessionsForUser_ExistingUserId_ReturnsWorkSessions()
        {
            var userId = 1; 

            var workSessions = _workSessionRepository.GetAllWorkSessionsForUser(userId);

            Assert.IsNotNull(workSessions);
        }

    }
}
