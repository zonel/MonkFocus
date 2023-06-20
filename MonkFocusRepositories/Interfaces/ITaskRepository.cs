using MonkFocusModels;

namespace MonkFocusRepositories.Interfaces;

public interface ITaskRepository
{
    public void AddTask(UserTask userTask);
    public void UpdateTask(UserTask userTask);
    public void DeleteTaskById(int TaskId);
    public IEnumerable<UserTask> GetAllTasksForUser(int userId);
}