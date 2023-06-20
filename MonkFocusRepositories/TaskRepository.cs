using MonkFocusDataAccess;
using MonkFocusModels;
using MonkFocusRepositories.Interfaces;

namespace MonkFocusRepositories
{
    public class TaskRepository : ITaskRepository
    {
        #region DI Fields
        private readonly MonkFocusDbContext _context;
        #endregion

        public TaskRepository(MonkFocusDbContext context)
        {
            _context = context;
        }
        public void AddTask(UserTask userTask)
        {
            if(userTask is null) throw new System.ArgumentNullException(nameof(userTask));

            _context.Tasks.Add(userTask);
            _context.SaveChanges();
        }

        public void UpdateTask(UserTask userTask)
        {
            if(userTask is null) throw new System.ArgumentNullException(nameof(userTask));

            _context.Tasks.Update(userTask);
            _context.SaveChanges();
        }

        public void DeleteTaskById(int TaskId)
        {
            var taskToDelete = _context.Tasks.FirstOrDefault(t => t.TaskId == TaskId);

            if (taskToDelete != null)
            {
                _context.Tasks.Remove(taskToDelete);
                _context.SaveChanges();
            }
        }

        public IEnumerable<UserTask> GetAllTasksForUser(int userId)
        {
            var userTasks = _context.Tasks.Where(t => t.UserId == userId).ToList();

            if (userTasks != null)return userTasks;

            return null;
        }
    }
}
