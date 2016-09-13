using PokerLeagueManager.Common;
using PokerLeagueManager.Common.Infrastructure;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.Infrastructure
{
    public interface IHandlesQuery<T>
        where T : IQuery
    {
        IQueryDataStore Repository { get; set; }

        object Execute(T query);
    }
}
