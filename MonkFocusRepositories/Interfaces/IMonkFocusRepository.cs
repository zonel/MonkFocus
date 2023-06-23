using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkFocusModels;

namespace MonkFocusRepositories.Interfaces
{
    public interface IMonkFocusRepository
    {
        public IEnumerable<User> GetTop3Leaderboard();

    }
}
