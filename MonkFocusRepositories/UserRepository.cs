using MonkFocusDataAccess;
using MonkFocusModels;
using MonkFocusRepositories.Interfaces;

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
            return _context.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public bool AuthenticateUser(string username, string password)
        {
            var userToValidate = _context.Users.FirstOrDefault(u => u.Username == username);

            if (userToValidate is null) return false;

            return userToValidate.Password == password;
        }
    }
}