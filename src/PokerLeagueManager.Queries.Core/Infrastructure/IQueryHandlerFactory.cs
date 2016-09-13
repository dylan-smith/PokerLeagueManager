using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Queries.Core.Infrastructure
{
    public interface IQueryHandlerFactory
    {
        object ExecuteQuery<T>(T query)
            where T : IQuery;

        object ExecuteQuery(IQuery query);
    }
}
