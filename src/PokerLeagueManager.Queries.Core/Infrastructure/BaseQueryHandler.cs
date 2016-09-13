using PokerLeagueManager.Common;

namespace PokerLeagueManager.Queries.Core.Infrastructure
{
    public class BaseQueryHandler
    {
        public IQueryDataStore Repository { get; set; }
    }
}
