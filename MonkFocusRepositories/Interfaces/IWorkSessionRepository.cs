using MonkFocusModels;

namespace MonkFocusRepositories.Interfaces;

public interface IWorkSessionRepository
{
    public void AddWorkSession(WorkSession workSession);
    public void DeleteWorkSessionById(int workSessionId);
    public IEnumerable<WorkSession> GetAllUsersWorkSessions(int userId);
}