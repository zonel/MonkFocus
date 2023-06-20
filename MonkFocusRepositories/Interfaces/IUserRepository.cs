using MonkFocusModels;

namespace MonkFocusRepositories.Interfaces;

public interface IUserRepository
{
    public void AddUser(User user);
    public void DeleteUserById(int userId);
    public User GetUserById(int id);
}