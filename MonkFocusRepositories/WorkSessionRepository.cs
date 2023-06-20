using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkFocusDataAccess;
using MonkFocusModels;
using MonkFocusRepositories.Interfaces;

namespace MonkFocusRepositories
{
    public class WorkSessionRepository : IWorkSessionRepository
    {
        #region DI Fields
        private readonly MonkFocusDbContext _context;
        #endregion

        public WorkSessionRepository(MonkFocusDbContext context)
        {
            _context = context;
        }
        public void AddWorkSession(WorkSession workSession)
        {
            if(workSession is null) throw new System.ArgumentNullException(nameof(workSession));

            _context.WorkSessions.Add(workSession);
            _context.SaveChanges();
        }

        public void DeleteWorkSessionById(int workSessionId)
        {
            var WorkSessionToDelete = _context.WorkSessions
                .FirstOrDefault(w => w.WorkSessionId == workSessionId);

            if (WorkSessionToDelete != null)
            {
                _context.WorkSessions.Remove(WorkSessionToDelete);
                _context.SaveChanges();
            }
        }

        public IEnumerable<WorkSession> GetAllUsersWorkSessions(int userId)
        {
            var usersWorkSessions = _context.WorkSessions
                .Where(w => w.UserId == userId)
                .ToList();

            if (usersWorkSessions is null) return null;

            return usersWorkSessions;
        }

    }
}
