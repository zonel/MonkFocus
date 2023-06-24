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
    public class MonkFocusRepository : IMonkFocusRepository
    {
        #region DI Fields
        private readonly MonkFocusDbContext _context;
        #endregion

        public MonkFocusRepository(MonkFocusDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetTop3Leaderboard()
        {
            return _context.Users.OrderByDescending(ws => ws.Points).Take(3).ToList();
        }

        /// <summary>
        /// Retrieves random quote from DB.
        /// </summary>
        /// <returns>Random quote in Quote data type </returns>
        public Quote GetRandomQuote()
        {
            Random random = new Random();
            var selectedNumber = random.Next(1, 102);
            return _context.Quotes.FirstOrDefault(q => q.QuoteId == selectedNumber) ?? throw new NullReferenceException();
        }
    }
}
