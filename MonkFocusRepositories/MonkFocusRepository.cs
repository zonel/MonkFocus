using MonkFocusDataAccess;
using MonkFocusModels;
using MonkFocusRepositories.Interfaces;

namespace MonkFocusRepositories;

/// <summary>
///     This class is used to retrieve general data from the database.
/// </summary>
public class MonkFocusRepository : IMonkFocusRepository
{
    #region DI Fields

    private readonly MonkFocusDbContext _context;

    #endregion

    public MonkFocusRepository(MonkFocusDbContext context)
    {
        _context = context;
    }

    /// <summary>
    ///     This method retrieves top 3 users with the most points.
    /// </summary>
    /// <returns>Collection of 3 top users</returns>
    public IEnumerable<User> GetTop3Leaderboard()
    {
        return _context.Users.OrderByDescending(ws => ws.Points).Take(3).ToList();
    }

    /// <summary>
    ///     Retrieves random quote from DB.
    /// </summary>
    /// <returns>Random quote in Quote data type </returns>
    public Quote GetRandomQuote()
    {
        var random = new Random();
        var selectedNumber = random.Next(1, 102);
        return _context.Quotes.FirstOrDefault(q => q.QuoteId == selectedNumber) ?? throw new NullReferenceException();
    }
}