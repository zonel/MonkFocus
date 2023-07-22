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
    public class UserRepositoryUnitTests
    {
        private UserRepository _userRepository;
        private MonkFocusMockDbContext _dbContext;

        [TestInitialize]
        public void Setup()
        {
            // Initialize the in-memory database and seed test data
            var options = new DbContextOptionsBuilder<MonkFocusMockDbContext>()
                .UseInMemoryDatabase(databaseName: "UnitTestDatabase")
                .Options;
            _dbContext = new MonkFocusMockDbContext(options);
            SeedTestData();

            // Create a new instance of UserRepository with the _dbContext
            _userRepository = new UserRepository(_dbContext);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Dispose of the _dbContext and set _userRepository to null
            _dbContext.Dispose();
            _userRepository = null;
        }

        [TestMethod]
        public void AddUser_ValidUser_UserAddedToDatabase()
        {
            // Arrange
            var user = new User
            {
                Username = "testuser",
                City = "testcity",
                Email = "TestEmail@email.com",
                Name = "Test Name",
                Password = "$2a$11$cWG/qsXtyTsacD3xvBa7nOMzN5OmIXLpzlL4eyIyCV2Tutl4pTHWm",
                // Set other properties as needed
            };

            // Act
            _userRepository.AddUser(user);

            // Assert
            var checkForUser = _dbContext.Users.FirstOrDefault(u => u.Username == "testuser");
            Assert.IsNotNull(checkForUser);
        }

        [TestMethod]
        public void DeleteUserById_ExistingUserId_UserDeletedFromDatabase()
        {
            // Arrange
            var userIdToDelete = 1; // Assuming a valid UserId exists in the database

            // Act
            _userRepository.DeleteUserById(userIdToDelete);

            // Assert
            var deletedUser = _dbContext.Users.FirstOrDefault(u => u.UserId == userIdToDelete);
            Assert.IsNull(deletedUser);
        }

        [TestMethod]
        public void GetUserById_ExistingUserId_ReturnsUser()
        {
            // Arrange
            var userId = 2; // Assuming a valid UserId exists in the database

            // Act
            var user = _userRepository.GetUserById(userId);

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual(userId, user.UserId);
        }

        [TestMethod]
        public void AuthenticateUser_ValidCredentials_ReturnsTrue()
        {
            // Arrange
            var username = "testuser";
            var password = "testpassword";

            // Act
            var result = _userRepository.AuthenticateUser(username, password);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AuthenticateUser_InvalidCredentials_ReturnsFalse()
        {
            // Arrange
            var username = "testuser";
            var password = "$2a$11$6cGIykg3XpsLH2J0xew21uZXp2JkI.Hwf5ALX8hLbGlwp75huZz/2";

            // Act
            var result = _userRepository.AuthenticateUser(username, password);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetUserByUsername_ExistingUsername_ReturnsUser()
        {
            // Arrange
            var username = "testuser";

            // Act
            var user = _userRepository.GetUserByUsername(username);

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual(username, user.Username);
        }

        [TestMethod]
        public void CheckIfUserExists_ExistingUsername_ReturnsTrue()
        {
            // Arrange
            var username = "testuser";

            // Act
            var result = _userRepository.CheckifUserExists(username);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckIfUserExists_NonExistingUsername_ReturnsFalse()
        {
            // Arrange
            var username = "nonexistinguser";

            // Act
            var result = _userRepository.CheckifUserExists(username);

            // Assert
           Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetTop3LatestSessionsForUser_ExistingUserId_ReturnsTop3Sessions()
        {
            // Arrange
            var userId = 2; // Assuming a valid UserId exists in the database

            // Act
            var sessions = _userRepository.GetTop3LatestSessionsForUser(userId);

            // Assert
            Assert.IsNotNull(sessions);
        }

        [TestMethod]
        public void GetUsersRemainingWorkTimeForToday_ExistingUserId_ReturnsRemainingWorkTime()
        {
            // Arrange
            var userId = 1; // Assuming a valid UserId exists in the database

            // Act
            var remainingWorkTime = _userRepository.GetUsersRemainingWorkTimeForToday(userId);

            // Assert
            Assert.IsNotNull(remainingWorkTime);
            // Add additional assertions as needed
        }

        [TestMethod]
        public void UpdateUsersWakeUpTime_ExistingUserId_WakeUpTimeUpdated()
        {
            // Arrange
            var userId = 2; // Assuming a valid UserId exists in the database
            var newWakeUpTime = new TimeOnly(6, 0, 0);

            // Act
            _userRepository.UpdateUsersWakeUpTime(userId, newWakeUpTime);

            // Assert
            var updatedUser = _dbContext.Users.FirstOrDefault(u => u.UserId == userId);
            Assert.IsNotNull(updatedUser);
            Assert.AreEqual(newWakeUpTime, updatedUser.WakeUpTime);
        }

        [TestMethod]
        public void UpdateUsersBedTime_ExistingUserId_BedTimeUpdated()
        {
            // Arrange
            var userId = 2; // Assuming a valid UserId exists in the database
            var newBedTime = new TimeOnly(22, 0, 0);

            // Act
            _userRepository.UpdateUsersBedTime(userId, newBedTime);

            // Assert
            var updatedUser = _dbContext.Users.FirstOrDefault(u => u.UserId == userId);
            Assert.IsNotNull(updatedUser);
            Assert.AreEqual(newBedTime, updatedUser.BedTime);
        }

        [TestMethod]
        public void UpdateUsersWorkTimeGoal_ExistingUserId_WorkTimeGoalUpdated()
        {
            // Arrange
            var userId = 2; // Assuming a valid UserId exists in the database
            var newWorkTimeGoal = new TimeSpan(6, 0, 0);

            // Act
            _userRepository.UpdateUsersWorkTimeGoal(userId, newWorkTimeGoal);

            // Assert
            var updatedUser = _dbContext.Users.FirstOrDefault(u => u.UserId == userId);
            Assert.IsNotNull(updatedUser);
            Assert.AreEqual(newWorkTimeGoal, updatedUser.WorkTimeGoal);
        }

        private void SeedTestData()
        {
            if (!_dbContext.Users.Any())
            {
                var testusers = new List<User>()
                {
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
                        Username = "adam",
                        Name = "Adam",
                        Password = "adam321",
                        City = "Katowice",
                        Email = "adam@gmail.com",
                        JoinDate = new System.DateTime(2023, 2, 25),
                        Points = 37,
                        WakeUpTime = new System.TimeOnly(6, 0, 0),
                        BedTime = new System.TimeOnly(22, 0, 0),
                        WorkTimeGoal = new System.TimeSpan(6, 0, 0),
                    },
                };
                 _dbContext.Users.AddRangeAsync(testusers);
            }
        }
    }
}
