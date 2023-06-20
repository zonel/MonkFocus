using Microsoft.EntityFrameworkCore;
using MonkFocusDataAccess;
using MonkFocusModels;
using MonkFocusRepositories;

namespace RepositoryUnitTests
{
    [TestClass]
    public class UserRepositoryTests
    {
        private static DbContextOptions<MonkFocusDbContext> _options;
        private static string _databaseName;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _databaseName = Guid.NewGuid().ToString();

            _options = new DbContextOptionsBuilder<MonkFocusDbContext>()
                .UseSqlite(_databaseName)
                .Options;
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            using (var context = new MonkFocusDbContext(_options))
            {
                context.Database.EnsureDeleted();
            }
        }

        [TestMethod]
        public void AddUser_Should_AddUserToDatabase()
        {
            // Arrange
            using (var context = new MonkFocusDbContext(_options))
            {
                var userRepository = new UserRepository(context);
                var user = new User { UserId = 1, UserName = "John" };

                // Act
                userRepository.AddUser(user);

                // Assert
                Assert.AreEqual(1, context.Users.Count());
                Assert.AreEqual("John", context.Users.Single().UserName);
            }
        }

        [TestMethod]
        public void DeleteUser_Should_RemoveUserFromDatabase()
        {
            // Arrange
            using (var context = new MonkFocusDbContext(_options))
            {
                var userRepository = new UserRepository(context);
                var user = new User { UserId = 1, UserName = "John" };
                context.Users.Add(user);
                context.SaveChanges();

                // Act
                userRepository.DeleteUser(user);

                // Assert
                Assert.AreEqual(0, context.Users.Count());
            }
        }
    }
}