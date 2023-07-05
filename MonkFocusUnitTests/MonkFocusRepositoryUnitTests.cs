using Microsoft.EntityFrameworkCore;
using MonkFocusDataAccess;
using MonkFocusModels;
using MonkFocusRepositories;
using Moq;

namespace MonkFocusUnitTests
{
    [TestClass]
    public class MonkFocusRepositoryTests
    {
        private MonkFocusMockDbContext _dbContext;
        private MonkFocusRepository _repository;
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
            _repository = null;
        }

        [TestMethod]
        public void GetTop3Leaderboard_ReturnsTop3UsersWithMostPoints()
        {
            _repository = new MonkFocusRepository(_dbContext);
            var result = _repository.GetTop3Leaderboard();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void GetRandomQuote_ReturnsRandomQuote()
        {
            _repository = new MonkFocusRepository(_dbContext);
            var result = _repository.GetRandomQuote();

            Assert.IsNotNull(result);
        }

        private void SeedTestData()
        {
            if (!_dbContext.Users.Any())
            {
                _dbContext.Users.AddRange(
                    new User
                    {
                        Username = "root",
                        Name = "Robert",
                        Password = "toor",
                        City = "Kraków",
                        Email = "root@root.com",
                        JoinDate = new System.DateTime(2019, 1, 1),
                        Points = 1337,
                        WakeUpTime = new System.TimeOnly(8, 0, 0),
                        BedTime = new System.TimeOnly(23, 0, 0),
                        WorkTimeGoal = new System.TimeSpan(8, 0, 0),
                    },
                    new User
                    {
                        Username = "root",
                        Name = "Robert",
                        Password = "toor",
                        City = "Kraków",
                        Email = "root@root.com",
                        JoinDate = new System.DateTime(2019, 1, 1),
                        Points = 1337,
                        WakeUpTime = new System.TimeOnly(8, 0, 0),
                        BedTime = new System.TimeOnly(23, 0, 0),
                        WorkTimeGoal = new System.TimeSpan(8, 0, 0),
                    },
                    new User
                    {
                        Username = "root",
                        Name = "Robert",
                        Password = "toor",
                        City = "Kraków",
                        Email = "root@root.com",
                        JoinDate = new System.DateTime(2019, 1, 1),
                        Points = 1337,
                        WakeUpTime = new System.TimeOnly(8, 0, 0),
                        BedTime = new System.TimeOnly(23, 0, 0),
                        WorkTimeGoal = new System.TimeSpan(8, 0, 0),
                    }
                );
            }

            if (!_dbContext.Quotes.Any())
            {
                _dbContext.Quotes.AddRange(
                    new Quote { QuoteId = 1, FullQuote = "Quote 1", Author = "Author1"},
                    new Quote { QuoteId = 2, FullQuote = "Quote 2", Author = "Author2" },
                    new Quote { QuoteId = 3, FullQuote = "Quote 3", Author = "Author3" }
                );
            }

            _dbContext.SaveChanges();
        }
    }
}